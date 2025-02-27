using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;
using MySqlConnector;
using System.Timers;

namespace Cloud_Sensor_DATA
{
    internal class SensorClient
    {
        private string ipAddr;
        private int port;
        private string cust;

        //데이터 송수신 관련
        private Dictionary<string,byte[]> protocolDict = new();
        private Dictionary<string, float> valueDict = new();
        private string? checkChannelCod;
        private byte checkSensorSlave;

        private Socket? SensorSock;
        private readonly AsyncObject obj;
        private static readonly DBHelper helper = new();

        //재연결 관련
        private bool reconnecting = false;
        private int reconnectAttempts = 0;
        private const int maxReconnectAttempts = 5;
        private const int reconnectDelay = 5000; // 5초 지연 후 재연결 시도

        private System.Timers.Timer sendProtocolTimer;
        public static int sendTime = 60000;
        public SensorClient(string PUBLIC_IP, string codCust)
        {
            ipAddr = PUBLIC_IP.Split(':')[0];
            port = int.Parse(PUBLIC_IP.Split(":")[1]);
            cust = codCust;
            ChannelSelect(PUBLIC_IP);
            SensorSock = CreateSocket();
            obj = new AsyncObject(8192);

            sendProtocolTimer = new System.Timers.Timer(sendTime) { AutoReset=true};
            sendProtocolTimer.Elapsed += (sender, e) => SendProtocol();
        }
        public void Start()
        {
            ConnServer();
        }
        private void ChannelSelect(string PUBLIC_IP)
        {
            MySqlParameter codCust = new MySqlParameter("@P_COD_CUST", MySqlDbType.VarChar) { Value = cust };
            MySqlParameter ipAddr = new MySqlParameter("@P_PUBLIC_IP", MySqlDbType.VarChar) { Value = PUBLIC_IP};
            var resultList = helper.ProcedureCall("CHANNEL_SELECT", codCust, ipAddr);
            foreach (var result in resultList)
            {
                //디바이스 MODEL PROTOCOL 확인용
                string codDevice = (string)result["COD_DEVICE"];
                //tem, hum과 같은 측정 값 분류
                string channelUnit = (string)result["CHANNEL_UNIT"];
                //1020-TEM-001 CHANNEL 고유 CODE
                string channelCod= (string)result["CHANNEL_COD"];
                byte[]? temByte = null;
                if (codDevice == "NO2" || codDevice == "SO2")
                {
                    temByte = new byte[Protocol.NO2_SO2_SENSOR.Length];
                    Array.Copy(Protocol.NO2_SO2_SENSOR, temByte, Protocol.NO2_SO2_SENSOR.Length);

                }
                else if (codDevice == "SEN0438" && channelUnit == "TEM")
                {
                    temByte = new byte[Protocol.TEM_SENSOR.Length];
                    Array.Copy(Protocol.TEM_SENSOR, temByte, Protocol.TEM_SENSOR.Length);
                }
                else if (codDevice == "SEN0438" && channelUnit == "HUM")
                {
                    temByte = new byte[Protocol.HUM_SENSOR.Length];
                    Array.Copy(Protocol.HUM_SENSOR, temByte, Protocol.HUM_SENSOR.Length);
                }
                /*프로토콜 주소 값 수정*/
                if(temByte != null)
                {
                    temByte[0] = Convert.ToByte(result["ADR_CHANNEL"]);
                    protocolDict[channelCod] = temByte;
                }
                valueDict[channelCod]=0f;
            }
        }
        private Socket CreateSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void ConnServer()
        {
            try
            {
                ResetSocket();

                IPAddress serverAddr = IPAddress.Parse(ipAddr);
                IPEndPoint clientEP = new IPEndPoint(serverAddr, port);

                SensorSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SensorSock.BeginConnect(clientEP, new AsyncCallback(ConnectCallback), SensorSock);
                Console($"{ipAddr}:{port}에 연결 시도 중");
            }
            catch (Exception ex)
            {
                Log($"{ipAddr}:{port} 연결 오류: {ex}");
                Reconnect();
            }
        }
        void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                if (SensorSock == null || !SensorSock.Connected)
                {
                    Log($"{ipAddr}:{port} 소켓이 null입니다.");
                    Reconnect();
                    return;
                }

                SensorSock.EndConnect(ar);

                if (SensorSock.Connected)
                {
                    obj.WorkingSocket = SensorSock;
                    Console($"{ipAddr}:{port} 서버에 연결됨.");
                    SensorSock?.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
                    sendProtocolTimer.Start();
                    SendProtocol();
                }
                else
                {
                    Log($"{ipAddr}:{port} 서버에 연결 실패.");
                    Reconnect();
                }
            }
            catch (Exception ex)
            {
                Log($"{ipAddr}:{port} ConnectCallback 오류: {ex.Message}");
                Reconnect();
            }
        }
        private void ResetSocket()
        {
            if (SensorSock != null)
            {
                SensorSock.Close();
            }

            SensorSock = CreateSocket();
        }
        private void Reconnect()
        {
            lock (protocolDict)
            {
                if (reconnecting || reconnectAttempts >= maxReconnectAttempts)
                {
                    Console($"{ipAddr}:{port} 최대 재연결 시도 횟수에 도달했거나 이미 재연결 중입니다.");
                    return;
                }

                reconnecting = true;
                reconnectAttempts++;
                Console($"{ipAddr}:{port} 재연결 중... 시도 #{reconnectAttempts}");

                try
                {
                    if (SensorSock != null)
                    {
                        SensorSock.Close();
                        SensorSock = null;
                    }

                    // 새 소켓 생성
                    ResetSocket();
                    Thread.Sleep(reconnectDelay); // 재연결 전 지연 추가
                    ConnServer();
                }
                catch (Exception ex)
                {
                    Log($"{ipAddr}:{port} 재연결 오류: {ex}");
                }
                finally
                {
                    reconnecting = false;
                }
            }
        }
        void DataReceived(IAsyncResult ar)
        {
            try
            {
                AsyncObject? obj = ar.AsyncState as AsyncObject;

                // 소켓 연결 상태 확인
                if (obj?.WorkingSocket == null || !obj.WorkingSocket.Connected)
                {
                    Log($"{ipAddr}:{port} 소켓이 연결되지 않았습니다.");
                    Reconnect(); // 연결되지 않았다면 재연결 시도
                    return;
                }

                int received = obj.WorkingSocket.EndReceive(ar);
                byte[] buffer = new byte[received];
                Array.Copy(obj.Buffer, 0, buffer, 0, received);

                ProcessData(buffer);

                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
            }
            catch (Exception ex)
            {
                Log($"{ipAddr}:{port} DataReceived 오류: {ex}");
            }
        }
        //CRC 계산
        private static ushort CalculateCRC(byte[] data, int length)
        {
            ushort crc = 0xFFFF;
            for (int i = 0; i < length; i++)
            {
                crc ^= (ushort)(data[i] & 0xFF);

                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }
        //프로토콜 전송 로직
        private async Task SendProtocol()
        {
            
            // 소켓 연결 상태 확인
            if (SensorSock == null || !SensorSock.Connected)
            {
                Log($"{ipAddr}:{port} 타이머가 동작 중인데 소켓이 연결되지 않았습니다. 재연결 시도.");
                Reconnect();
                return;
            }
            // 상태 확인을 위한 valueDict 초기화
            foreach(var value in valueDict)
            {
                valueDict[value.Key] = -1000;
            }
            foreach (var sendData in protocolDict)
            {
                // CRC 계산
                ushort crc = CalculateCRC(sendData.Value, sendData.Value.Length);
                byte crcLow = (byte)(crc & 0xFF);
                byte crcHigh = (byte)((crc >> 8) & 0xFF);

                // CRC를 추가한 전송용 데이터 배열 생성
                byte[] sendDataWithCRC = new byte[sendData.Value.Length + 2];
                Array.Copy(sendData.Value, 0, sendDataWithCRC, 0, sendData.Value.Length);
                sendDataWithCRC[sendData.Value.Length] = crcLow;
                sendDataWithCRC[sendData.Value.Length + 1] = crcHigh;
                lock (protocolDict)
                {
                    // CHENNEL_COD 및 Slave주소 확인을 위한 변수 할당
                    checkChannelCod = sendData.Key;
                    checkSensorSlave = sendData.Value[0];
                };
                // 변환된 데이터 로그 출력
                string sendDataString = BitConverter.ToString(sendDataWithCRC);
                Console($"{ipAddr}:{port}의 {checkChannelCod} 변환 데이터 : {sendDataString}");

                try
                {
                    // 데이터 전송
                    SensorSock.Send(sendDataWithCRC);
                    // 데이터 수신 대기
                    SensorSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
                    await Task.Delay(500);
                }
                catch (Exception ex)
                {
                    // 예외 처리
                    Log($"{ipAddr}:{port} 데이터 전송 오류: {ex}");
                    Reconnect();
                }
            }
            helper.InsertValue(valueDict, cust);
        }
        // 수신 프로토콜을 string 형식으로 변환
        public string ByteToHex(byte[] hex)
        {
            StringBuilder result = new StringBuilder(hex.Length * 2);
            foreach (byte b in hex)
            {
                result.Append(b.ToString("x2").ToUpper());
            }
            return result.ToString();
        }
        // 수신 프로토콜에서 value 값 추출 로직
        public string ConvertValue(string msg)
        {
            string check = checkSensorSlave.ToString("x2").ToUpper();

            if (msg.Length > 14) 
            {
                if(msg.Length % 14 == 0)
                {
                    for (int i = 0; i < msg.Length / 14; i++)
                    {
                        if (msg.Substring(i * 14, 14).StartsWith(check))
                        {
                            msg = msg.Substring(i * 14, 14);
                        }
                    }
                }else if(msg.Length % 18 == 0)
                {
                    for (int i = 0; i < msg.Length / 18; i++)
                    {
                        if (msg.Substring(i * 18, 18).StartsWith(check))
                        {
                            msg = msg.Substring(i * 18, 18);
                        }
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(msg), $"{checkChannelCod}의 값이 아닙니다.");
                }
            }
            else if(!msg.StartsWith(check))
            {
                throw new ArgumentOutOfRangeException(nameof(msg), $"{checkChannelCod}의 값이 아닙니다.");
            }
            CheckProtocolError(msg);
            Console($"{ipAddr}:{port} {checkChannelCod} 데이터 수신: {msg}");

            string con = ValidationMSG(msg);

            return con;
        }
        //프로토콜 길이 차이에 따른 읽는 값 수정 로직
        private string ValidationMSG(string msg)
        {
            string con;
            if (msg.Length == 18 && checkChannelCod?.Split('-')[1] == "TEM")
            {
                con = msg.Substring(10, 4);
            }
            else
            {
                con = msg.Substring(6, 4);
            }

            return con;
        }
        // 프로토콜 오류로그 확인 로직
        private void CheckProtocolError(string msg)
        {
            string errorText="";
            switch (msg.Substring(4, 2))
            {
                case "01": errorText = "Illegal Function"; break;
                case "02": errorText = "Illegal Data Address"; break;
                case "03": errorText = "Illegal Data Value"; break;
                case "04": errorText = "Server Device Failure"; break;
                case "05": errorText = "Acknowledge"; break;
                case "06": errorText = "Server Device Busy"; break;
                case "08": errorText = "Memory Parity Error"; break;
                case "0A": errorText = "Gateway Path Unavailable"; break;
                case "0B": errorText = "Gateway Target Device Failed to Respond"; break;
            }
            if(msg.Substring(2,1) == "8")
            {
                Console($"Error Code : {msg.Substring(4, 2)}({errorText}");
                helper.InsertState(checkChannelCod, msg.Substring(4, 2),errorText );
                throw new ArgumentOutOfRangeException(nameof(msg), $"Error Code : {msg.Substring(4,2)}({errorText}");
            }
        }
        // msg변환 후 16진수에서 10진수로 변경
        public float HextoFloat(string num)
        {
            int intRep = int.Parse(num, NumberStyles.AllowHexSpecifier); // 16진수 문자열을 정수로 파싱
            float f = intRep;// 실제 값으로 변환하기 위해 10으로 나누기
            f = ValidationValue(f);

            return f;
        }
        // 센서별 측정범위 설정
        public float ValidationValue(float f)
        {
            if (checkChannelCod?.Split('-')[1] == "TEM" || checkChannelCod?.Split('-')[1] == "HUM")
            {
                f /= 10.0f;
                if (f > 100)
                {
                    f = -1000;
                }
            }
            else if (checkChannelCod?.Split('-')[1] == "NO2" || checkChannelCod?.Split('-')[1] == "SO2")
            {
                if (f > 2000 || f<0)
                {
                    f = -1000;
                }
            }
            return f;
        }
        // 수신 데이터 변환 후 Dict 할당
        void ProcessData(byte[] buffer)
        {
            var value = HextoFloat(ConvertValue(ByteToHex(buffer)));
            if (checkChannelCod != null)
            {
                valueDict[checkChannelCod] = value;
            }
        }

        private void Log(string message)
        {
            // 디버그 출력 (파일이나 다른 목적지에 로그 기록)
            Debug.WriteLine($"{DateTime.Now}: {message}");
        }
        private void Console(string message)
        {
            // 디버그 출력 (파일이나 다른 목적지에 로그 기록)
            System.Console.WriteLine($"{DateTime.Now}: {message}");
        }
        public class AsyncObject
        {
            public byte[] Buffer;
            public Socket? WorkingSocket;
            public readonly int BufferSize;

            public AsyncObject(int bufferSize)
            {
                BufferSize = bufferSize;
                Buffer = new byte[BufferSize];
            }

            public void ClearBuffer()
            {
                Array.Clear(Buffer, 0, BufferSize);
            }
        }
    }
}