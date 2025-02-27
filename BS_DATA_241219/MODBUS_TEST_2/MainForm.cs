using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MODBUS_TEST_2
{
    public partial class MainForm : Form
    {
        Sensor1 sen1 = new Sensor1();
        Sensor2 sen2 = new Sensor2();
        Sensor3 sen3 = new Sensor3();
        Sensor4 sen4 = new Sensor4();
        Sensor5 sen5 = new Sensor5();
        DBHelper helper = new DBHelper();

        public MainForm()
        {
            Sensor1_Load();
            Sensor2_Load();
            Sensor3_Load();
            Sensor4_Load();
            Sensor5_Load();

            InitializeComponent();
            Tx_TimeSet.Text = BS_SENSOR_DATA.Properties.Settings.Default.TimerSet;
            TimerSetting();
        }

        private void Sensor1_Load()
        {
            sen1.TopLevel = false;
            this.Controls.Add(sen1);
            sen1.StartPosition = FormStartPosition.Manual;
            sen1.Location = new Point(17, 10);
            sen1.Show();
        }

        private void Sensor2_Load()
        {
            sen2.TopLevel = false;
            this.Controls.Add(sen2);
            sen2.StartPosition = FormStartPosition.Manual;
            sen2.Location = new Point(17, 210);
            sen2.Show();
        }
        private void Sensor3_Load()
        {
            sen3.TopLevel = false;
            this.Controls.Add(sen3);
            sen3.StartPosition = FormStartPosition.Manual;
            sen3.Location = new Point(170,290);
            sen3.Show();
        }
        private void Sensor4_Load()
        {
            sen4.TopLevel = false;
            this.Controls.Add(sen4);
            sen4.StartPosition = FormStartPosition.Manual;
            sen4.Location = new Point(1053, 210);
            sen4.Show();
        }
        private void Sensor5_Load()
        {
            sen5.TopLevel = false;
            this.Controls.Add(sen5);
            sen5.StartPosition = FormStartPosition.Manual;
            sen5.Location = new Point(17, 290);
            sen5.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            helper.Disconnect();
            helper.Query(sen1.perValues, sen1.temValues, sen2.preValues, sen3.eleValues,sen4.preValues,new float[1] {0},new float[2] {0,0 });
            helper.Query_2(sen3.resultList,
                           sen5.resultList,
                           new List<float[]> {
                               new float[2] {0,0},//kwh
                               new float[2] {0,0},//v
                               new float[2] {0,0},//a
                               new float[2] {0,0}//w
                           });
        }

        private void Bt_Setting_Click(object sender, EventArgs e)
        {
            TimerSetting();
            BS_SENSOR_DATA.Properties.Settings.Default.TimerSet = Tx_TimeSet.Text;
            BS_SENSOR_DATA.Properties.Settings.Default.Save();
        }

        private void Tx_TimeSet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void TimerSetting()
        {
            try
            {
                if (string.IsNullOrEmpty(Tx_TimeSet.Text) || Tx_TimeSet.Text == "0")
                {
                    Tx_TimeSet.Text = "5";
                }

                int x = Convert.ToInt32(Tx_TimeSet.Text);
                int set = x * 60000;

                int sen1Timer2Interval = set - 1900;
                int sen2Timer2Interval = set - 1800;
                int sen3Timer2Interval = set - 1700;
                int sen4Timer2Interval = set - 1600;
                int sen5Timer2Interval = set - 1500;

                if (sen1Timer2Interval > 0)
                {
                    sen1.timer2.Interval = sen1Timer2Interval;
                    sen2.timer2.Interval = sen2Timer2Interval;
                    sen3.timer2.Interval = sen3Timer2Interval;
                    sen4.timer2.Interval = sen4Timer2Interval;
                    sen5.timer2.Interval = sen5Timer2Interval;

                    timer1.Interval = set;

                    timer1.Start();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("타이머 설정 값이 너무 작습니다.");
                }
            }
            catch (FormatException ex)
            {
                Log($"타이머 설정 오류: {ex.Message}");
                Tx_TimeSet.Text = "5";
                TimerSetting();
            }
            catch (Exception ex)
            {
                Log($"타이머 설정 중 예외 발생: {ex.Message}");
            }
        }

        private void Log(string message)
        {
            // 디버그 출력 (로그 파일 등)
            Debug.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
