using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YJ_SENSOR_DATA;

namespace MODBUS_TEST_2
{
    public partial class Sensor3 : Form
    {
        SocketConnecter socketConnecter = null;
        bool sendCheck = false;

        public float[] RPMValues = new float[5] {0,0,0,0,0 };
        public float[] KhValues = new float[5] { 0, 0, 0, 0, 0 };
        public float[] eleValues = new float[5] { 0, 0, 0, 0, 0 };
        public float[] amValues = new float[5] { 0, 0, 0, 0, 0 };
        List<Label> rpmLabelList= new List<Label>();
        List<Label> kwLabelList = new List<Label>();
        private List<int> pendingRequests = new List<int>();
        private DBHelper dbHelper = new DBHelper();
        public int delayTime = 60000;
        // 데이터 전송을 위한 바이트 배열 정의
        private List<byte[]> sendDataList = new List<byte[]> {
                        new byte[] { 0x00, 0x04, 0x00, 0x14, 0x00, 0x01 },//RPM
                        new byte[] { 0x00, 0x04, 0x00, 0x0C, 0x00, 0x02 },//전력 kh
                        new byte[] { 0x00, 0x04, 0x00, 0x08, 0x00, 0x03 },//전류 am
                        new byte[] { 0x00, 0x04, 0x00, 0x0A, 0x00, 0x04 },//전압 ele
                    };
        public Sensor3()
        {
            InitializeComponent();
            socketConnecter = new SocketConnecter("172.30.1.57", 8899, "인버터");

            socketConnecter.OnConnected += () =>
            {
                Log("sen 3 소켓 연결 성공 이벤트 발생. 타이머 시작.");
                sendCheck = true;

                _ = SendProtocol();
            };
            socketConnecter.OnDataReceived += ProcessData;
            // 연결 해제시 타이머 종료
            socketConnecter.DisConnected += () =>
            {
                sendCheck = false;
            };
            LoadForm();
            // 연결 성공 시 타이머 시작
            _ = socketConnecter.ConnectServerAsync();
        }
        private void LoadForm()
        {
            for (int i = 0; i < 5; i++)
            {
                this.Controls.Add(createPanel(i));
            }

        }
        // panel 만들기
        private Panel createPanel(int i)
        {
            int x = i* (210 +20);
            int y = 0;
            Panel panel = new Panel();
            panel.Size = new Size(210, 232);
            panel.BackColor = Color.White;
            panel.Location = new Point(x, y);

            Label textBox = new Label();
            textBox.BorderStyle = BorderStyle.None;
            textBox.Location = new Point(0,0);
            textBox.Size = new Size(210, 31);
            textBox.Font = new Font("굴림", 20F, FontStyle.Bold);
            textBox.BackColor = Color.FromArgb(0xf7,0xf7,0xf7);
            textBox.Text = $"인버터 {i + 1}";
            textBox.TextAlign=ContentAlignment.MiddleCenter;

            PictureBox rpmIcon = new PictureBox();
            rpmIcon.Size = new Size(90, 90);
            rpmIcon.Padding = new Padding(10);
            rpmIcon.BackColor = Color.LightGray;
            rpmIcon.Location = new Point(4,36);
            rpmIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            rpmIcon.Image = YJ_SENSOR_DATA.Properties.Resources.RPM;

            PictureBox kwIcon = new PictureBox();
            kwIcon.Size = new Size(90, 90);
            kwIcon.Padding = new Padding(10);
            kwIcon.BackColor = Color.LightGray;
            kwIcon.Location = new Point(4, 132);
            kwIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            kwIcon.Image = YJ_SENSOR_DATA.Properties.Resources.power;

            Label rpmLabel = new Label();
            rpmLabel.Font = new Font("굴림", 20F, FontStyle.Bold);
            rpmLabel.AutoSize = true;
            rpmLabel.Text = "RPM";
            rpmLabel.Location = new Point(96, 51);

            Label rpmValue= new Label();
            rpmValue.Font = new Font("굴림", 20F, FontStyle.Bold);
            rpmValue.AutoSize = true;
            rpmValue.Location = new Point(96, 81);
            rpmValue.Text = "0";

            Label kwLabel = new Label();
            kwLabel.Font = new Font("굴림", 20F, FontStyle.Bold);
            kwLabel.AutoSize = true;
            kwLabel.Text = "전력";
            kwLabel.Location = new Point(96, 147);

            Label kwValue = new Label();
            kwValue.Font = new Font("굴림", 20F, FontStyle.Bold);
            kwValue.AutoSize = true;
            kwValue.Text = "0 kw";
            kwValue.Location = new Point(96, 177);

            panel.Controls.Add(textBox);
            panel.Controls.Add(rpmIcon);
            panel.Controls.Add(kwIcon);
            panel.Controls.Add(rpmLabel);
            panel.Controls.Add(rpmValue);
            panel.Controls.Add(kwLabel);
            panel.Controls.Add(kwValue);

            rpmLabelList.Add(rpmValue);
            kwLabelList.Add(kwValue);

            panel.Paint += (sender, e) =>
            {
                int borderSize = 1; // 테두리 두께
                Color borderColor = Color.FromArgb(0xda, 0xda, 0xda); // 테두리 색상

                using (Pen pen = new Pen(borderColor, borderSize))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, panel.Width - 1, panel.Height - 1));
                }
            };
            return panel;
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

        public string Convert_Per(string msg,int slaveCheck, string protocolName, int dataLength)
        {
            Log($"sen 3 Addr : {slaveCheck} {protocolName} 수신: {msg}");
            if (string.IsNullOrEmpty(msg) || msg.Length < 10 + dataLength * 2)
            {
                throw new ArgumentOutOfRangeException(nameof(msg), "sen 3 입력 문자열이 너무 짧습니다.");
            }
            if (msg.Length % (10 + dataLength * 2) != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(msg), "sen 3 입력 문자열이 올바르지 않습니다.");
            }

            for (var i = 0; i < msg.Length; i += 10 + dataLength * 2)
            {
                var trimmedMsg = msg.Substring(i, 10 + dataLength * 2);
                if (trimmedMsg.StartsWith($"0{slaveCheck}"))
                {
                    string con = trimmedMsg.Substring(6, 4);
                    Log($"sen 3 Addr : {slaveCheck} {protocolName} 변환: {con}");
                    return con;
                }
            }
            throw new ArgumentOutOfRangeException(nameof(msg), "해당 주소로 시작하는 유효한 메시지를 찾을 수 없습니다.");
        }

        public float HextoFloat(string num)
        {
            
            int intRep = int.Parse(num, NumberStyles.AllowHexSpecifier); // 16진수 문자열을 정수로 파싱
            float f = intRep;
            return f;
        }

        void ProcessData(byte[] buffer)
        {
            try
            {
                int slaveId = buffer[0];
                int dataLength = buffer[2];
                int requestId = (slaveId << 8) | dataLength;
                string protocolName = "";
                float value = 0;
                if (pendingRequests.Contains(requestId))
                {
                    switch (dataLength)
                    {
                        case 2:
                            protocolName = "RPM";
                            value = HextoFloat(Convert_Per(ByteToHex(buffer), slaveId, protocolName, dataLength));
                            RPMValues[slaveId - 1] = value;
                            break;
                        case 4:
                            protocolName = "전력";
                            value = HextoFloat(Convert_Per(ByteToHex(buffer), slaveId, protocolName, dataLength));
                            KhValues[slaveId - 1] = value;
                            break;
                        case 6:
                            protocolName = "전류";
                            value = HextoFloat(Convert_Per(ByteToHex(buffer), slaveId, protocolName, dataLength));
                            amValues[slaveId - 1] = value;
                            break;
                        case 8:
                            protocolName = "전압";
                            value = HextoFloat(Convert_Per(ByteToHex(buffer), slaveId, protocolName, dataLength));
                            eleValues[slaveId - 1] = value;
                            break;
                    }
                    Log($"sen 3 Addr : {slaveId} {protocolName} 변환 값 :{value}");
                    pendingRequests.Remove(requestId);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Log($"sen 3 데이터 수신 오류 : {ex}");
            }catch(IndexOutOfRangeException ex)
            {
                Log($"sen 3 데이터 수신 오류 : {ex}");
            }
            
        }
        private void UpdatePerTextBox()
        {
            if (InvokeRequired)
            {
                for(var i =0; i < 5; i++)
                {
                    Invoke(new Action(() => rpmLabelList[i].Text = RPMValues[i].ToString()));
                    Invoke(new Action(() => kwLabelList[i].Text = KhValues[i].ToString() + " kW"));
                }
                
            }
            else
            {
                for (var i = 0; i < 10; i++)
                {
                    kwLabelList[i].Text = KhValues[i].ToString()+ " kW";
                    rpmLabelList[i].Text = RPMValues[i].ToString();
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

        private async Task SendProtocol()
        {
            try
            {

                while (sendCheck)
                {
                    for (var j = 0; j < sendDataList.Count; j++)
                    {
                        for (var i = 1; i <= 5; i++)
                        {
                            sendDataList[j][0] = (byte)i;
                            // CRC 계산
                            ushort crc = CalculateCRC(sendDataList[j], sendDataList[j].Length);
                            byte crcLow = (byte)(crc & 0xFF);
                            byte crcHigh = (byte)((crc >> 8) & 0xFF);

                            // CRC를 추가한 전송용 데이터 배열 생성
                            byte[] sendDataWithCRC = new byte[sendDataList[j].Length + 2];
                            Array.Copy(sendDataList[j], 0, sendDataWithCRC, 0, sendDataList[j].Length);
                            sendDataWithCRC[sendDataList[j].Length] = crcLow;
                            sendDataWithCRC[sendDataList[j].Length + 1] = crcHigh;

                            // 변환된 데이터 로그 출력
                            string sendDataString = BitConverter.ToString(sendDataWithCRC);
                            int requestId = (i << 8) | (j+1)*2; // 요청 id 생성 slave + 데이터 길이
                            pendingRequests.Add(requestId);
                            // 데이터 전송
                            await socketConnecter.SendAsync(sendDataWithCRC);
                            Log($"sen 3 전송 데이터 {i} : {sendDataString}");
                            await Task.Delay(300);
                        }
                    }
                    UpdatePerTextBox();
                    dbHelper.insertInverter(RPMValues, "RPM");
                    dbHelper.insertInverter(KhValues, "KW");
                    dbHelper.insertInverter(amValues, "AM");
                    dbHelper.insertInverter(eleValues, "ELE");
                    await Task.Delay(delayTime);
                }
            }
            catch (Exception ex)
            {
                Log($"데이터 전송 오류: {ex}");
                dbHelper.inesrtError(ex.GetType().ToString(), "Data Send 에러 - " + ex.Message, "인버터");
                await socketConnecter.DisconnectAsync();
            }
        }

        private void Log(string message)
        {
            // 디버그 출력 (파일이나 다른 목적지에 로그 기록)
            Debug.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
