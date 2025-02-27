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

        DBHelper helper = new DBHelper();

        public MainForm()
        {
            LoadChildForm(sen1, 20, 60);
            LoadChildForm(sen2, 610, 60);
            LoadChildForm(sen3, 20, 220);
            LoadChildForm(sen4, 20, 482);
            
            helper.Connect();
            InitializeComponent();
            Tx_TimeSet.Text = YJ_SENSOR_DATA.Properties.Settings.Default.TimerSet;
            TimerSetting();
        }
        private void LoadChildForm(Form childForm, int x, int y)
        {
            Panel panel = new Panel();
            panel.Size = new Size(childForm.Size.Width+20, childForm.Size.Height+20);
            panel.BackColor = Color.White;
            panel.BorderStyle= BorderStyle.None;
            panel.Location = new Point(x, y);

            childForm.TopLevel = false;
            childForm.Location = new Point(10,10);
            panel.Controls.Add(childForm);
            this.Controls.Add(panel);

            childForm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            helper.Disconnect();
            helper.Query(sen1.perValue, sen2.perValue);
        }

        private void Bt_Setting_Click(object sender, EventArgs e)
        {
            TimerSetting();
            YJ_SENSOR_DATA.Properties.Settings.Default.TimerSet = Tx_TimeSet.Text;
            YJ_SENSOR_DATA.Properties.Settings.Default.Save();
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

                if (set >= 60000)
                {
                    sen3.delayTime = set-1500;
                    sen4.delayTime = set-1500;

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
