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
using System.Drawing;
using System.Reflection;

namespace MODBUS_TEST_2
{
    public partial class Sensor1 : Form
    {
        int slaveCheck = 0;
        SocketConnecter socketConnecter;

        public float[] perValues = new float[10];
        public float[] temValues = new float[10];
        TextBox[] txPerArray = new TextBox[10];
        TextBox[] txTemArray = new TextBox[10];
        DBHelper helper = new DBHelper();
        Random rand = new Random(); // 값 확인 시 제거

        public Sensor1()
        {
            InitializeComponent();
            socketConnecter = new SocketConnecter("172.30.1.58", 8899, "sen 1");
            // 연결 성공 시 타이머 시작
            socketConnecter.OnConnected += () =>
            {
                Log("sen 1 소켓 연결 성공 이벤트 발생. 타이머 시작.");
                socketConnecter.obj.WorkingSocket.BeginReceive(socketConnecter.obj.Buffer, 0, socketConnecter.obj.BufferSize, 0, DataReceived, socketConnecter.obj);
                timer2_Tick(null, EventArgs.Empty);

                if (InvokeRequired)
                {
                    Log($"sen 1 Invoke 호출 시작 : {timer2.Enabled}");
                    Invoke(new Action(() =>
                    {
                        if (!timer2.Enabled)
                        {
                            Log("sen 1 타이머 시작(Invoke)");
                            timer2.Start();
                        }
                    }));
                }
                else
                {
                    if (!timer2.Enabled)
                    {
                        Log("sen 1 타이머 시작");
                        timer2.Start();
                    }
                }
            };
            // 연결 해제 시 타이머 스탑
            socketConnecter.DisConnected += () =>
            {
                timer2.Stop();
            };
            txPerArray[0] = Tx_Per_1;
            txPerArray[1] = Tx_Per_2;
            txPerArray[2] = Tx_Per_3;
            txPerArray[3] = Tx_Per_4;
            txPerArray[4] = Tx_Per_5;
            txPerArray[5] = Tx_Per_6;
            txPerArray[6] = Tx_Per_7;
            txPerArray[7] = Tx_Per_8;
            txPerArray[8] = Tx_Per_9;
            txPerArray[9] = Tx_Per_10;

            txTemArray[0] = Tx_Tem_1;
            txTemArray[1] = Tx_Tem_2;
            txTemArray[2] = Tx_Tem_3;
            txTemArray[3] = Tx_Tem_4;
            txTemArray[4] = Tx_Tem_5;
            txTemArray[5] = Tx_Tem_6;
            txTemArray[6] = Tx_Tem_7;
            txTemArray[7] = Tx_Tem_8;
            txTemArray[8] = Tx_Tem_9;
            txTemArray[9] = Tx_Tem_10;
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

                Log($"Sen 1 데이터 받음 - 수신된 데이터: {received}, 버퍼: {BitConverter.ToString(receivedData)}");
                if (slaveCheck <= 10)
                {
                    ProcessData(receivedData);
                }


                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);

            }
            catch (ObjectDisposedException)
            {
                // 소켓이 폐기되었음, 아무것도 수행하지 않거나 이벤트를 로깅합니다.
                Log("Sen 1 소켓이 폐기되었습니다.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Log($"Sen 1 DataReceived 오류: {ex}");
                socketConnecter.obj.WorkingSocket.BeginReceive(socketConnecter.obj.Buffer, 0, socketConnecter.obj.BufferSize, 0, DataReceived, socketConnecter.obj);
            }
            catch (Exception ex)
            {
                Log($"Sen 1 DataReceived 오류: {ex}");
                helper.inesrtError(ex.GetType().ToString(), "DataReceived 에러 - " + ex.Message, "sen 1");

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

        public string[] ConvertData(string msg)
        {
            if (msg == null || msg.Length < 18)
            {
                throw new ArgumentOutOfRangeException(nameof(msg), "입력 문자열이 너무 짧습니다.");
            }
            if (msg.Length > 18)
            {
                int startCheck = msg.IndexOf($"0{slaveCheck}0304");
                if (startCheck != -1 && msg.Length - startCheck >= 18)
                {
                    msg = msg.Substring(startCheck, 18);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(msg), "문자열이 형식에 맞지 않습니다..");
                }
            }


            string temMsg = msg.Substring(10, 4); // 습도 또는 온도 계산을 위한 첫 번째 부분 추출
            string perMsg = msg.Substring(6, 4); // 습도 또는 온도 계산을 위한 다음 부분 추출

            Log($"온도 데이터{slaveCheck}변환: {temMsg}");
            Log($"습도 데이터{slaveCheck}변환: {perMsg}");

            return new string[2] { temMsg, perMsg };
        }

        public float HextoFloat(string num)
        {
            int intRep = int.Parse(num, NumberStyles.AllowHexSpecifier); // 16진수 문자열을 정수로 파싱
            float f = intRep / 10.0f; // 실제 값으로 변환하기 위해 10으로 나누기
            if (f > 100)
            {
                throw new ArgumentOutOfRangeException("데이터 범주 값을 벗어났습니다.");
            }
            return f;
        }
        void ProcessData(byte[] buffer)
        {
            string[] result = ConvertData(ByteToHex(buffer));
            temValues[0] = HextoFloat(result[0]);
            perValues[0] = HextoFloat(result[1]);

            for (int i = 1; i < perValues.Length; i++)
            {

                perValues[i] = (float)Math.Round(rand.NextDouble() - 0.5, 1) + perValues[0];
                temValues[i] = (float)Math.Round(rand.NextDouble() - 0.5, 1) + temValues[0];

                Log($"처리된 데이터 습도{i+1} : {perValues[i]}");
                Log($"처리된 데이터 온도{i+1} : {temValues[i]}");
            }
            UpdatePerTextBox();
        }

        private void UpdatePerTextBox()
        {
            for (int i = 0; i < perValues.Length; i++)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => txPerArray[i].Text = perValues[i].ToString("F1")));
                    Invoke(new Action(() => txTemArray[i].Text = temValues[i].ToString("F1")));
                }
                else
                {
                    txPerArray[i].Text = perValues[i].ToString("F1");
                    txTemArray[i].Text = temValues[i].ToString("F1");
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
            slaveCheck = 0;
            // 소켓 연결 상태 확인
            lock (socketConnecter.lockObject)
            {
                if (!socketConnecter.CheckSocketState()) return;
            }

            List<byte[]> sendDataList = new List<byte[]>
             {
               new byte[] { 0x01, 0x03, 0x00, 0x00, 0x00, 0x02 }
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

                // 변환된 데이터 로그 출력
                string sendDataString = BitConverter.ToString(sendDataWithCRC);
                slaveCheck++;
                Log($"온습도 전송 데이터 {slaveCheck} : {sendDataString}");

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
