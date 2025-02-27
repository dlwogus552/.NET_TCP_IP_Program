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

namespace MODBUS_TEST_2
{
    public partial class Sensor4 : Form
    {
        int slaveCheck = 0;
        SocketConnecter socketConnecter;

        public float[] preValues = new float[2];
        TextBox[] txPreArray = new TextBox[2];
        DBHelper helper = new DBHelper();

        public Sensor4()
        {
            InitializeComponent();
            socketConnecter = new SocketConnecter("172.30.1.220", 8899, "sen 2-2");
            // 연결 성공 시 타이머 시작
            socketConnecter.OnConnected += () =>
            {
                Log("sen 2-2 소켓 연결 성공 이벤트 발생. 타이머 시작.");
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
            // 연결 해제 시 타이머 스탑
            socketConnecter.DisConnected += () =>
            {
                timer2.Stop();
            };
            txPreArray[0] = Tx_Pre_1;
            txPreArray[1] = Tx_Pre_2;
        }

        void DataReceived(IAsyncResult ar)
        {
            try
            {
                AsyncObject obj = (AsyncObject)ar.AsyncState;

                lock (socketConnecter.lockObject)
                {
                    if (!socketConnecter.CheckSocketState()) return;
                }

                int received = socketConnecter.obj.WorkingSocket.EndReceive(ar);
                byte[] receivedData = new byte[received];
                Array.Copy(obj.Buffer, 0, receivedData, 0, received);

                Log($"Sen 2-2 데이터 받음 - 수신된 데이터: {received}, 버퍼: {BitConverter.ToString(receivedData)}");
                if (slaveCheck <= 2)
                {
                    ProcessData(receivedData);
                }
                
                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);

            }
            catch (ObjectDisposedException)
            {
                // 소켓이 폐기되었음, 아무것도 수행하지 않거나 이벤트를 로깅합니다.
                Log("Sen 2-2 소켓이 폐기되었습니다.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Log($"Sen 2-2 DataReceived 오류: {ex}");
                socketConnecter.obj.WorkingSocket.BeginReceive(socketConnecter.obj.Buffer, 0, socketConnecter.obj.BufferSize, 0, DataReceived, socketConnecter.obj);
            }
            catch (Exception ex)
            {
                Log($"Sen 2-2 DataReceived 오류: {ex}");

                helper.inesrtError(ex.GetType().ToString(), "DataReceived 에러 - " + ex.Message,"sen 2-2");

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
            return result.ToString();
        }

        public string ConvertData(string msg)
        {
            if (msg == null || msg.Length < 14)
            {
                throw new ArgumentOutOfRangeException(nameof(msg), "압력 입력 문자열이 너무 짧습니다.");
            }
            if (msg.Length > 14)
            {
                int startCheck = msg.IndexOf($"0{slaveCheck}0402");
                if (startCheck != -1 && msg.Length - startCheck >= 14)
                {
                    msg = msg.Substring(startCheck, 14);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(msg), "문자열이 형식에 맞지 않습니다..");
                }
            }
            string con = msg.Substring(6, 4); // 습도 또는 온도 계산을 위한 첫 번째 부분 추출
            Log($"압력 데이터{slaveCheck}변환: {con}");
            return con;
        }

        public float HextoFloat(string num)
        {
            int intRep = int.Parse(num, NumberStyles.AllowHexSpecifier); // 16진수 문자열을 정수로 파싱
            float f = intRep / 100.00f; // 실제 값으로 변환하기 위해 100으로 나누기
            return f;
        }
        void ProcessData(byte[] buffer)
        {
            string msg = ConvertData(ByteToHex(buffer));
            int index = slaveCheck - 1;
            float f= HextoFloat(msg);
            if(f>= 0 && f <= 10)
            {
                preValues[index] = f;
            }

            Log($"처리된 압력 데이터{slaveCheck} : {preValues[index]}");

            UpdatePreTextBox(index, preValues[index]);
        }

        private void UpdatePreTextBox(int index, float value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => txPreArray[index].Text = value.ToString()));
            }
            else
            {
                txPreArray[index].Text = value.ToString();
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
            slaveCheck = 0;
            // 소켓 연결 상태 확인
            lock (socketConnecter.lockObject)
            {
                if (!socketConnecter.CheckSocketState()) return;
            }

            List<byte[]> sendDataList = new List<byte[]>
             {
               new byte[] { 0x01, 0x04, 0x00, 0x00, 0x00, 0x01 },
               new byte[] { 0x02, 0x04, 0x00, 0x00, 0x00, 0x01 }
             };

            foreach (var sendData in sendDataList)
            {
                // CRC 계산
                ushort crc = CalculateCRC(sendData, sendData.Length);
                byte crcLow = (byte)(crc & 0xFF);
                byte crcHigh = (byte)((crc >> 8) & 0xFF);

                // CRC를 추가한 전송용 데이터 배열 생성
                byte[] sendDataWithCRC = new byte[sendData.Length + 2];
                Array.Copy(sendData, 0, sendDataWithCRC, 0, sendData.Length);
                sendDataWithCRC[sendData.Length] = crcLow;
                sendDataWithCRC[sendData.Length + 1] = crcHigh;
                slaveCheck++;
                // 변환된 데이터 로그 출력
                string sendDataString = BitConverter.ToString(sendDataWithCRC);
                Log($"압력 전송 데이터{slaveCheck}: {sendDataString}");

                try
                {
                    // 데이터 전송
                    socketConnecter.obj.WorkingSocket.Send(sendDataWithCRC);
                    // 전송 후 대기
                    Thread.Sleep(300);
                }
                catch (Exception ex)
                {
                    // 예외 처리
                    Log($"데이터 전송 오류: {ex}");
                    helper.inesrtError(ex.GetType().ToString(), "Data Send 에러 - " + ex.Message, "sen 1");

                    lock (socketConnecter.lockObject)
                    {
                        socketConnecter.DisConnect();
                        socketConnecter.Reconnect();
                    }
                }
            }
        }

        private void Log(string message)
        {
            // 디버그 출력 (파일이나 다른 목적지에 로그 기록)
            Debug.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
