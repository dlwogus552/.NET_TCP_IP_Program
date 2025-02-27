using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Text;

namespace MODBUS_TEST_2
{
    public partial class Sensor3 : Form
    {
        int[] slaveCheck = new int[7] { 4, 19, 11, 1, 2, 17, 16 };
        int slave = 0;
        int protocolCehck = 0;
        string protocolName;
        string resultMsg = "";
        SocketConnecter socketConnecter;
        public List<float[]> resultList = new List<float[]>();
        public DateTime checkMonth;
        public float[] eleValues = new float[7];
        public float[] lastEleValues = new float[7]; // 저번달
        public float[] voltValues = new float[7];
        public float[] ampeorValues = new float[7];
        public float[] wattValues = new float[7];

        TextBox[] txEleArray = new TextBox[7];

        public int status = 0;

        private DBHelper helper = new DBHelper();

        public Sensor3()
        {
            InitializeComponent();
            socketConnecter = new SocketConnecter("172.30.1.59", 8899, "sen 3");
            // 연결 성공 시 타이머 시작
            socketConnecter.OnConnected += () =>
            {
                Log("sen 3 소켓 연결 성공 이벤트 발생. 타이머 시작.");
                socketConnecter.obj.WorkingSocket.BeginReceive(socketConnecter.obj.Buffer, 0, socketConnecter.obj.BufferSize, 0, DataReceived, socketConnecter.obj);
                timer2_Tick(null, EventArgs.Empty);
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        if (!timer2.Enabled)
                        {
                            timer2.Start();
                        }
                    }));
                }
                else
                {
                    if (!timer2.Enabled)
                    {
                        timer2.Start();
                    }
                }
            };
            lastEleValues = helper.Query_CalcCfKwh();
            checkMonth = DateTime.Now;
            // 연결 해제 시 타이머 스탑
            socketConnecter.DisConnected += () =>
            {
                timer2.Stop();
            };

            txEleArray[0] = Tx_Ele_1;
            txEleArray[1] = Tx_Ele_2;
            txEleArray[2] = Tx_Ele_3;
            txEleArray[3] = Tx_Ele_4;
            txEleArray[4] = Tx_Ele_5;
            txEleArray[5] = Tx_Ele_6;
            txEleArray[6] = Tx_Ele_7;
        }

        void DataReceived(IAsyncResult ar)
        {

            try
            {
                AsyncObject obj = (AsyncObject)ar.AsyncState;
                // 소켓 연결 상태 확인
                lock (socketConnecter.lockObject)
                {
                    if (!socketConnecter.CheckSocketState()) return;
                }

                int received = socketConnecter.obj.WorkingSocket.EndReceive(ar);
                byte[] receivedData = new byte[received];
                Array.Copy(obj.Buffer, 0, receivedData, 0, received);


                Log($"sen 3 Addr : {slave} {protocolName}받음 - 수신된 데이터: {received}, 버퍼: {BitConverter.ToString(receivedData)}");
                ProcessData(receivedData);
                socketConnecter.obj.WorkingSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
            }
            catch (ObjectDisposedException)
            {
                // 소켓이 폐기되었음, 아무것도 수행하지 않거나 이벤트를 로깅합니다.
                Log("sen 3 소켓이 폐기되었습니다.");
            }
            catch (ArgumentOutOfRangeException Ae)
            {
                Log($"sen 3 DataReceived 오류 : {Ae}");
                socketConnecter.obj.WorkingSocket.BeginReceive(socketConnecter.obj.Buffer, 0, socketConnecter.obj.BufferSize, 0, DataReceived, socketConnecter.obj);

            }
            catch (Exception ex)
            {
                Log($"sen 3 DataReceived 오류: {ex}");
                helper.inesrtError(ex.GetType().ToString(), "DataReceived 에러 - " + ex.Message, "전력량계");
                lock (socketConnecter.lockObject)
                {
                    socketConnecter.DisConnect();
                    socketConnecter.Reconnect();
                }
            }
        }
        public string ByteToHex(byte[] hex)
        {
            StringBuilder result = new StringBuilder(hex.Length * 2);
            foreach (byte b in hex)
            {
                result.Append(b.ToString("x2").ToUpper());
            }
            Log($"sen 3 Addr : {slave} {protocolName} 수신: {result.ToString()}");

            return result.ToString();
        }

        private float validConvertData(float data, string name, int index)
        {
            float[] values = null;
            int maxValue = 0;
            switch (name)
            {
                case "V":
                    values = voltValues;
                    maxValue = 250;
                    break;
                case "kwh":
                    values = eleValues;
                    maxValue = 65000;
                    break;
                case "W":
                    values = wattValues;
                    maxValue = 65000;
                    break;
                case "A":
                    values = ampeorValues;
                    maxValue = 65000;
                    break;
            }
            if(data >= maxValue)
            {
                data = values[index];
            }
            


            return data;
        }
        
        public float HextoFloat(string num, string name, int index)
        {
            int intRep = int.Parse(num, NumberStyles.AllowHexSpecifier);
            float f = BitConverter.ToSingle(BitConverter.GetBytes(intRep), 0);

            f = validConvertData(f,name,index);

            return f;
        }

        void ProcessData(byte[] buffer)
        {
            resultMsg += ByteToHex(buffer);
        }

        private void UpdateEleTextBox()
        {
            if (InvokeRequired)
            {
                for (int i = 0; i < 7; i++)
                {
                    Invoke(new Action(() => txEleArray[i].Text = (eleValues[i] - lastEleValues[i]).ToString("F2")));
                }
                    
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    txEleArray[i].Text = (eleValues[i] - lastEleValues[i]).ToString("F2");
                }
            }
        }



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

        private void timer2_Tick(object sender, EventArgs e)
        {
            resultList.Clear();
            resultMsg = "";
            // 소켓 연결 상태 확인
            lock (socketConnecter.lockObject)
            {
                if (!socketConnecter.CheckSocketState()) return;
            }
            if(DateTime.Now.Month != checkMonth.Month)
            {
                lastEleValues = helper.Query_CalcCfKwh();
            }


            // 각 장치별로 전송할 프로토콜 리스트
            List<byte[]> sendDataList = new List<byte[]>
            {
                new byte[] { 0x00, 0x04, 0x01, 0x00, 0x00, 0x05 }, // kwh
                new byte[] { 0x00, 0x04, 0x00, 0x00, 0x00, 0x02 }, // V
                new byte[] { 0x00, 0x04, 0x00, 0x08, 0x00, 0x03 }, // A
                new byte[] { 0x00, 0x04, 0x00, 0x12, 0x00, 0x04 } // W
            };
            for (var j = 0; j < sendDataList.Count; j++)
            {
                protocolCehck = j;
                switch (protocolCehck)
                {
                    case 0:
                        protocolName = "전체전력(KWH)";
                        break;
                    case 1:
                        protocolName = "볼트(V)";
                        break;
                    case 2:
                        protocolName = "전류(A)";
                        break;
                    case 3:
                        protocolName = "전력(W)";
                        break;
                }
                for (int i = 0; i < slaveCheck.Length; i++)
                {
                    var sendData = sendDataList[j];
                    sendData[0] =(byte)slaveCheck[i];
                    slave = slaveCheck[i];
                    // CRC 계산
                    ushort crc = CalculateCRC(sendData, sendData.Length);
                    byte crcLow = (byte)(crc & 0xFF);
                    byte crcHigh = (byte)((crc >> 8) & 0xFF);

                    // CRC를 추가한 전송용 데이터 배열 생성
                    byte[] sendDataWithCRC = new byte[sendData.Length + 2];
                    Array.Copy(sendData, 0, sendDataWithCRC, 0, sendData.Length);
                    sendDataWithCRC[sendData.Length] = crcLow;
                    sendDataWithCRC[sendData.Length + 1] = crcHigh;

                    // 변환된 데이터 로그 출력
                    string sendDataString = BitConverter.ToString(sendDataWithCRC);
                    Log($"sen 3 전송 데이터 : {sendDataString}");

                    try
                    {
                        // 데이터 전송
                        socketConnecter.obj.WorkingSocket.Send(sendDataWithCRC);

                        Thread.Sleep(300);
                    }
                    catch (Exception ex)
                    {
                        Log($"데이터 전송 오류: {ex}");
                        helper.inesrtError(ex.GetType().ToString(), "Data Send 에러 - " + ex.Message, "전력량계");
                        lock (socketConnecter.lockObject)
                        {
                            socketConnecter.DisConnect();
                            socketConnecter.Reconnect();
                        }
                    }
                }
            }
            splitResult();
        }
        private void splitResult()
        {
            try
            {
                ParseValues("0A", eleValues, "kwh", 30);
                ParseValues("04", voltValues, "V",18);
                ParseValues("06", ampeorValues, "A", 22);
                ParseValues("08", wattValues, "W", 26);
            }
            catch (ArgumentException ae)
            {
                Log($"sen 3 timer2_tick 에러 : {ae}");
            }
            UpdateEleTextBox();
            resultList.Add(eleValues);
            resultList.Add(voltValues);
            resultList.Add(ampeorValues);
            resultList.Add(wattValues);

        }
        private void ParseValues(string suffix, float[] values, string unit, int receiveDataLength)
        {
            int start = FindStartIndex(suffix);

            if (start == -1) return;

            string msg = resultMsg.Substring(start);
            for (int i = 0; i < 7; i++)
            {
                string checkAddr = ((byte)slaveCheck[i]).ToString("X2");
                int valueStart = msg.IndexOf($"{checkAddr}04{suffix}");
                if (valueStart != -1 && msg.Length - valueStart >= receiveDataLength)
                {
                    values[i] = HextoFloat(msg.Substring(valueStart, receiveDataLength).Substring(6, 8), unit, i);
                    Log($"처리한 {unit} 데이터 {i + 1} : {values[i]}");
                    msg = msg.Substring(valueStart + receiveDataLength);
                }
            }
        }
        private int FindStartIndex(string suffix)
        {
            int result = 0;
            for (int i = 0; i <7;  i++)
            {
                string checkAddr = ((byte)slaveCheck[i]).ToString("X2");
                result = resultMsg.IndexOf($"{checkAddr}04{suffix}");
                if (result != -1) break;
            }
            return result;
        }
        private void Log(string message)
        {
            // 디버그 출력 (파일이나 다른 목적지에 로그 기록)
            Debug.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
