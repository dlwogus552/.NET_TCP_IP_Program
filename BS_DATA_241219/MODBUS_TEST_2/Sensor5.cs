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
    public partial class Sensor5 : Form
    {
        string resultMsg = "";
        SocketConnecter socketConnecter;

        public float[] eleValues = new float[1] { 0};
        public float[] voltValues = new float[1] { 0};
        public float[] ampeorValues = new float[1] {0 };
        public float[] wattValues = new float[1] {0 };
        public List<float[]> resultList = new List<float[]>();

        TextBox[] txPreArray = new TextBox[1];
        DBHelper helper = new DBHelper();

        public Sensor5()
        {
            InitializeComponent();
            socketConnecter = new SocketConnecter("172.30.1.60", 8899, "sen 3-2");
            // 연결 성공 시 타이머 시작
            socketConnecter.OnConnected += () =>
            {
                Log("sen 3-2 소켓 연결 성공 이벤트 발생. 타이머 시작.");
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

                Log($"sen 3-2 데이터 받음 - 수신된 데이터: {received}, 버퍼: {BitConverter.ToString(receivedData)}");

                ProcessData(receivedData);

                
                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);

            }
            catch (ObjectDisposedException)
            {
                // 소켓이 폐기되었음, 아무것도 수행하지 않거나 이벤트를 로깅합니다.
                Log("sen 3-2 소켓이 폐기되었습니다.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Log($"sen 3-2 DataReceived 오류: {ex}");
                socketConnecter.obj.WorkingSocket.BeginReceive(socketConnecter.obj.Buffer, 0, socketConnecter.obj.BufferSize, 0, DataReceived, socketConnecter.obj);
            }
            catch (Exception ex)
            {
                Log($"sen 3-2 DataReceived 오류: {ex}");
                helper.inesrtError(ex.GetType().ToString(), "DataReceived 에러 - " + ex.Message,"sen 3-2");
                
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
            string con1 = msg.Substring(0, 2);
            string con2 = msg.Substring(2, 2);
            string con3 = msg.Substring(4, 2);
            string con4 = msg.Substring(6, 2);
            string con = con4 + con3 + con2 + con1;

            Log($"sen 3-2 데이터 변환: {con}");

            return con;
        }
        private float validConvertData(float data, string name, int index)
        {
            float[] values = null;
            int maxValue = 0;
            switch (name)
            {
                case "V":
                    values = voltValues;
                    maxValue = 400;
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
            if (data >= maxValue)
            {
                data = values[index];
            }

            return data;
        }
        public float HextoFloat(string num,string unit, int index)
        {
            int intRep = int.Parse(num, NumberStyles.AllowHexSpecifier); // 16진수 문자열을 정수로 파싱
            float f = intRep / 1000.00f; // 실제 값으로 변환하기 위해 100으로 나누기
            f = validConvertData(f, unit, index);
            return f;
        }
        void ProcessData(byte[] buffer)
        {
            resultMsg += ByteToHex(buffer);
        }

        private void UpdatePreTextBox(float value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => txPreArray[0].Text = value.ToString()));
            }
            else
            {
                txPreArray[0].Text = value.ToString();
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

            // 소켓 연결 상태 확인
            lock (socketConnecter.lockObject)
            {
                if (!socketConnecter.CheckSocketState()) return;
            }

            List<byte[]> sendDataList = new List<byte[]>
             {
               new byte[] { 0x01, 0x04, 0x00, 0x00, 0x00, 0x02 },//KWH
               new byte[] { 0x01, 0x04, 0x00, 0x1C, 0x00, 0x03 },//V
               new byte[] { 0x01, 0x04, 0x00, 0x22, 0x00, 0x04 },//A
               new byte[] { 0x01, 0x04, 0x00, 0x04, 0x00, 0x05 },//W
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
                Log($"sen 3-2 전송 데이터 : {sendDataString}");
                
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
                    helper.inesrtError(ex.GetType().ToString(), "Data Send 에러 - " + ex.Message, "sen 3-2");

                    timer2.Stop();
                    lock (socketConnecter.lockObject)
                    {
                        socketConnecter.DisConnect();
                        socketConnecter.Reconnect();
                    }
                }
            }
            splitResult();
        }
        private void splitResult()
        {
            try
            {
                ParseValues("04", eleValues, "kwh", 18);
                ParseValues("06", voltValues, "V", 22);
                ParseValues("08", ampeorValues, "A", 26);
                ParseValues("0A", wattValues, "W", 30);
            }
            catch (ArgumentException ae)
            {
                Log($"sen 3-2 timer2_tick 에러 : {ae}");
            }
            UpdatePreTextBox(eleValues[0]);
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

            int valueStart = msg.IndexOf($"0104{suffix}");
            if (valueStart != -1 && msg.Length - valueStart >= receiveDataLength)
            {

                values[0] = HextoFloat(ConvertData(msg.Substring(valueStart, receiveDataLength).Substring(6, 8)), unit, 0);
                Log($"sen 3-2 처리한 {unit} 데이터 : {values[0]}");
                msg = msg.Substring(valueStart + receiveDataLength);
            }
        }
        private int FindStartIndex(string suffix)
        {
            int result = 0;

            result = resultMsg.IndexOf($"0104{suffix}");

            return result;
        }
        private void Log(string message)
        {
            // 디버그 출력 (파일이나 다른 목적지에 로그 기록)
            Debug.WriteLine($"{DateTime.Now}: {message}");
        }

    }
}
