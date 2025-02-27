namespace MODBUS_TEST_2
{
    partial class Sensor1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.Tx_Per_1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Tx_Tem_1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Tx_Tem_2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Tx_Per_2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Tx_Tem_3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Tx_Per_3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Tx_Tem_4 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Tx_Per_4 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.Tx_Tem_5 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Tx_Per_5 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Tx_Tem_6 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.Tx_Per_6 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.Tx_Tem_7 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.Tx_Per_7 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.Tx_Tem_8 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.Tx_Per_8 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.Tx_Tem_9 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.Tx_Per_9 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.Tx_Tem_10 = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.Tx_Per_10 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 18F);
            this.label1.Location = new System.Drawing.Point(34, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 24);
            this.label1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 18F);
            this.label2.Location = new System.Drawing.Point(42, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "습도(%)";
            // 
            // timer2
            // 
            this.timer2.Interval = 60000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Tx_Per_1
            // 
            this.Tx_Per_1.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Per_1.Location = new System.Drawing.Point(30, 80);
            this.Tx_Per_1.Name = "Tx_Per_1";
            this.Tx_Per_1.ReadOnly = true;
            this.Tx_Per_1.Size = new System.Drawing.Size(120, 30);
            this.Tx_Per_1.TabIndex = 6;
            this.Tx_Per_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 20F);
            this.label3.Location = new System.Drawing.Point(27, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 27);
            this.label3.TabIndex = 7;
            this.label3.Text = "device_1";
            // 
            // Tx_Tem_1
            // 
            this.Tx_Tem_1.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Tem_1.Location = new System.Drawing.Point(30, 160);
            this.Tx_Tem_1.Name = "Tx_Tem_1";
            this.Tx_Tem_1.ReadOnly = true;
            this.Tx_Tem_1.Size = new System.Drawing.Size(120, 30);
            this.Tx_Tem_1.TabIndex = 8;
            this.Tx_Tem_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 18F);
            this.label4.Location = new System.Drawing.Point(42, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 24);
            this.label4.TabIndex = 4;
            this.label4.Text = "온도(℃)";
            // 
            // Tx_Tem_2
            // 
            this.Tx_Tem_2.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Tem_2.Location = new System.Drawing.Point(156, 160);
            this.Tx_Tem_2.Name = "Tx_Tem_2";
            this.Tx_Tem_2.ReadOnly = true;
            this.Tx_Tem_2.Size = new System.Drawing.Size(120, 30);
            this.Tx_Tem_2.TabIndex = 13;
            this.Tx_Tem_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 20F);
            this.label5.Location = new System.Drawing.Point(153, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 27);
            this.label5.TabIndex = 12;
            this.label5.Text = "device_2";
            // 
            // Tx_Per_2
            // 
            this.Tx_Per_2.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Per_2.Location = new System.Drawing.Point(156, 80);
            this.Tx_Per_2.Name = "Tx_Per_2";
            this.Tx_Per_2.ReadOnly = true;
            this.Tx_Per_2.Size = new System.Drawing.Size(120, 30);
            this.Tx_Per_2.TabIndex = 11;
            this.Tx_Per_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 18F);
            this.label6.Location = new System.Drawing.Point(168, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 24);
            this.label6.TabIndex = 9;
            this.label6.Text = "온도(℃)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("굴림", 18F);
            this.label7.Location = new System.Drawing.Point(168, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 24);
            this.label7.TabIndex = 10;
            this.label7.Text = "습도(%)";
            // 
            // Tx_Tem_3
            // 
            this.Tx_Tem_3.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Tem_3.Location = new System.Drawing.Point(282, 160);
            this.Tx_Tem_3.Name = "Tx_Tem_3";
            this.Tx_Tem_3.ReadOnly = true;
            this.Tx_Tem_3.Size = new System.Drawing.Size(120, 30);
            this.Tx_Tem_3.TabIndex = 18;
            this.Tx_Tem_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("굴림", 20F);
            this.label8.Location = new System.Drawing.Point(279, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 27);
            this.label8.TabIndex = 17;
            this.label8.Text = "device_3";
            // 
            // Tx_Per_3
            // 
            this.Tx_Per_3.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Per_3.Location = new System.Drawing.Point(282, 80);
            this.Tx_Per_3.Name = "Tx_Per_3";
            this.Tx_Per_3.ReadOnly = true;
            this.Tx_Per_3.Size = new System.Drawing.Size(120, 30);
            this.Tx_Per_3.TabIndex = 16;
            this.Tx_Per_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("굴림", 18F);
            this.label9.Location = new System.Drawing.Point(294, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 24);
            this.label9.TabIndex = 14;
            this.label9.Text = "온도(℃)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("굴림", 18F);
            this.label10.Location = new System.Drawing.Point(294, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 24);
            this.label10.TabIndex = 15;
            this.label10.Text = "습도(%)";
            // 
            // Tx_Tem_4
            // 
            this.Tx_Tem_4.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Tem_4.Location = new System.Drawing.Point(408, 160);
            this.Tx_Tem_4.Name = "Tx_Tem_4";
            this.Tx_Tem_4.ReadOnly = true;
            this.Tx_Tem_4.Size = new System.Drawing.Size(120, 30);
            this.Tx_Tem_4.TabIndex = 23;
            this.Tx_Tem_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("굴림", 20F);
            this.label11.Location = new System.Drawing.Point(405, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(124, 27);
            this.label11.TabIndex = 22;
            this.label11.Text = "device_4";
            // 
            // Tx_Per_4
            // 
            this.Tx_Per_4.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Per_4.Location = new System.Drawing.Point(408, 80);
            this.Tx_Per_4.Name = "Tx_Per_4";
            this.Tx_Per_4.ReadOnly = true;
            this.Tx_Per_4.Size = new System.Drawing.Size(120, 30);
            this.Tx_Per_4.TabIndex = 21;
            this.Tx_Per_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("굴림", 18F);
            this.label12.Location = new System.Drawing.Point(420, 125);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 24);
            this.label12.TabIndex = 19;
            this.label12.Text = "온도(℃)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("굴림", 18F);
            this.label13.Location = new System.Drawing.Point(420, 45);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 24);
            this.label13.TabIndex = 20;
            this.label13.Text = "습도(%)";
            // 
            // Tx_Tem_5
            // 
            this.Tx_Tem_5.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Tem_5.Location = new System.Drawing.Point(534, 160);
            this.Tx_Tem_5.Name = "Tx_Tem_5";
            this.Tx_Tem_5.ReadOnly = true;
            this.Tx_Tem_5.Size = new System.Drawing.Size(120, 30);
            this.Tx_Tem_5.TabIndex = 28;
            this.Tx_Tem_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("굴림", 20F);
            this.label14.Location = new System.Drawing.Point(531, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(124, 27);
            this.label14.TabIndex = 27;
            this.label14.Text = "device_5";
            // 
            // Tx_Per_5
            // 
            this.Tx_Per_5.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Per_5.Location = new System.Drawing.Point(534, 80);
            this.Tx_Per_5.Name = "Tx_Per_5";
            this.Tx_Per_5.ReadOnly = true;
            this.Tx_Per_5.Size = new System.Drawing.Size(120, 30);
            this.Tx_Per_5.TabIndex = 26;
            this.Tx_Per_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("굴림", 18F);
            this.label15.Location = new System.Drawing.Point(546, 125);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 24);
            this.label15.TabIndex = 24;
            this.label15.Text = "온도(℃)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("굴림", 18F);
            this.label16.Location = new System.Drawing.Point(546, 45);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(95, 24);
            this.label16.TabIndex = 25;
            this.label16.Text = "습도(%)";
            // 
            // Tx_Tem_6
            // 
            this.Tx_Tem_6.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Tem_6.Location = new System.Drawing.Point(660, 160);
            this.Tx_Tem_6.Name = "Tx_Tem_6";
            this.Tx_Tem_6.ReadOnly = true;
            this.Tx_Tem_6.Size = new System.Drawing.Size(120, 30);
            this.Tx_Tem_6.TabIndex = 33;
            this.Tx_Tem_6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("굴림", 20F);
            this.label17.Location = new System.Drawing.Point(657, 10);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(124, 27);
            this.label17.TabIndex = 32;
            this.label17.Text = "device_6";
            // 
            // Tx_Per_6
            // 
            this.Tx_Per_6.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Per_6.Location = new System.Drawing.Point(660, 80);
            this.Tx_Per_6.Name = "Tx_Per_6";
            this.Tx_Per_6.ReadOnly = true;
            this.Tx_Per_6.Size = new System.Drawing.Size(120, 30);
            this.Tx_Per_6.TabIndex = 31;
            this.Tx_Per_6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("굴림", 18F);
            this.label18.Location = new System.Drawing.Point(672, 125);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(98, 24);
            this.label18.TabIndex = 29;
            this.label18.Text = "온도(℃)";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("굴림", 18F);
            this.label19.Location = new System.Drawing.Point(672, 45);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(95, 24);
            this.label19.TabIndex = 30;
            this.label19.Text = "습도(%)";
            // 
            // Tx_Tem_7
            // 
            this.Tx_Tem_7.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Tem_7.Location = new System.Drawing.Point(786, 160);
            this.Tx_Tem_7.Name = "Tx_Tem_7";
            this.Tx_Tem_7.ReadOnly = true;
            this.Tx_Tem_7.Size = new System.Drawing.Size(120, 30);
            this.Tx_Tem_7.TabIndex = 38;
            this.Tx_Tem_7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("굴림", 20F);
            this.label20.Location = new System.Drawing.Point(783, 10);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(124, 27);
            this.label20.TabIndex = 37;
            this.label20.Text = "device_7";
            // 
            // Tx_Per_7
            // 
            this.Tx_Per_7.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Per_7.Location = new System.Drawing.Point(786, 80);
            this.Tx_Per_7.Name = "Tx_Per_7";
            this.Tx_Per_7.ReadOnly = true;
            this.Tx_Per_7.Size = new System.Drawing.Size(120, 30);
            this.Tx_Per_7.TabIndex = 36;
            this.Tx_Per_7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("굴림", 18F);
            this.label21.Location = new System.Drawing.Point(798, 125);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(98, 24);
            this.label21.TabIndex = 34;
            this.label21.Text = "온도(℃)";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("굴림", 18F);
            this.label22.Location = new System.Drawing.Point(798, 45);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(95, 24);
            this.label22.TabIndex = 35;
            this.label22.Text = "습도(%)";
            // 
            // Tx_Tem_8
            // 
            this.Tx_Tem_8.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Tem_8.Location = new System.Drawing.Point(912, 160);
            this.Tx_Tem_8.Name = "Tx_Tem_8";
            this.Tx_Tem_8.ReadOnly = true;
            this.Tx_Tem_8.Size = new System.Drawing.Size(120, 30);
            this.Tx_Tem_8.TabIndex = 43;
            this.Tx_Tem_8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("굴림", 20F);
            this.label23.Location = new System.Drawing.Point(909, 10);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(124, 27);
            this.label23.TabIndex = 42;
            this.label23.Text = "device_8";
            // 
            // Tx_Per_8
            // 
            this.Tx_Per_8.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Per_8.Location = new System.Drawing.Point(912, 80);
            this.Tx_Per_8.Name = "Tx_Per_8";
            this.Tx_Per_8.ReadOnly = true;
            this.Tx_Per_8.Size = new System.Drawing.Size(120, 30);
            this.Tx_Per_8.TabIndex = 41;
            this.Tx_Per_8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("굴림", 18F);
            this.label24.Location = new System.Drawing.Point(924, 125);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(98, 24);
            this.label24.TabIndex = 39;
            this.label24.Text = "온도(℃)";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("굴림", 18F);
            this.label25.Location = new System.Drawing.Point(924, 45);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(95, 24);
            this.label25.TabIndex = 40;
            this.label25.Text = "습도(%)";
            // 
            // Tx_Tem_9
            // 
            this.Tx_Tem_9.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Tem_9.Location = new System.Drawing.Point(1038, 160);
            this.Tx_Tem_9.Name = "Tx_Tem_9";
            this.Tx_Tem_9.ReadOnly = true;
            this.Tx_Tem_9.Size = new System.Drawing.Size(120, 30);
            this.Tx_Tem_9.TabIndex = 48;
            this.Tx_Tem_9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("굴림", 20F);
            this.label26.Location = new System.Drawing.Point(1035, 10);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(124, 27);
            this.label26.TabIndex = 47;
            this.label26.Text = "device_9";
            // 
            // Tx_Per_9
            // 
            this.Tx_Per_9.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Per_9.Location = new System.Drawing.Point(1038, 80);
            this.Tx_Per_9.Name = "Tx_Per_9";
            this.Tx_Per_9.ReadOnly = true;
            this.Tx_Per_9.Size = new System.Drawing.Size(120, 30);
            this.Tx_Per_9.TabIndex = 46;
            this.Tx_Per_9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("굴림", 18F);
            this.label27.Location = new System.Drawing.Point(1050, 125);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(98, 24);
            this.label27.TabIndex = 44;
            this.label27.Text = "온도(℃)";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("굴림", 18F);
            this.label28.Location = new System.Drawing.Point(1050, 45);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(95, 24);
            this.label28.TabIndex = 45;
            this.label28.Text = "습도(%)";
            // 
            // Tx_Tem_10
            // 
            this.Tx_Tem_10.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Tem_10.Location = new System.Drawing.Point(1164, 160);
            this.Tx_Tem_10.Name = "Tx_Tem_10";
            this.Tx_Tem_10.ReadOnly = true;
            this.Tx_Tem_10.Size = new System.Drawing.Size(120, 30);
            this.Tx_Tem_10.TabIndex = 53;
            this.Tx_Tem_10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("굴림", 20F);
            this.label29.Location = new System.Drawing.Point(1161, 10);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(140, 27);
            this.label29.TabIndex = 52;
            this.label29.Text = "device_10";
            // 
            // Tx_Per_10
            // 
            this.Tx_Per_10.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Per_10.Location = new System.Drawing.Point(1164, 80);
            this.Tx_Per_10.Name = "Tx_Per_10";
            this.Tx_Per_10.ReadOnly = true;
            this.Tx_Per_10.Size = new System.Drawing.Size(120, 30);
            this.Tx_Per_10.TabIndex = 51;
            this.Tx_Per_10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("굴림", 18F);
            this.label30.Location = new System.Drawing.Point(1176, 125);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(98, 24);
            this.label30.TabIndex = 49;
            this.label30.Text = "온도(℃)";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("굴림", 18F);
            this.label31.Location = new System.Drawing.Point(1176, 45);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(95, 24);
            this.label31.TabIndex = 50;
            this.label31.Text = "습도(%)";
            // 
            // Sensor1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 192);
            this.Controls.Add(this.Tx_Tem_10);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.Tx_Per_10);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.Tx_Tem_9);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.Tx_Per_9);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.Tx_Tem_8);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.Tx_Per_8);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.Tx_Tem_7);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.Tx_Per_7);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.Tx_Tem_6);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.Tx_Per_6);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.Tx_Tem_5);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.Tx_Per_5);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.Tx_Tem_4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.Tx_Per_4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Tx_Tem_3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Tx_Per_3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Tx_Tem_2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Tx_Per_2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Tx_Tem_1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Tx_Per_1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Sensor1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox Tx_Per_1;
        public System.Windows.Forms.Timer timer2;
        public System.Windows.Forms.TextBox Tx_Tem_1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox Tx_Tem_2;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox Tx_Per_2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox Tx_Tem_3;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox Tx_Per_3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox Tx_Tem_4;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox Tx_Per_4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox Tx_Tem_5;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox Tx_Per_5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox Tx_Tem_6;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.TextBox Tx_Per_6;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.TextBox Tx_Tem_7;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.TextBox Tx_Per_7;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        public System.Windows.Forms.TextBox Tx_Tem_8;
        private System.Windows.Forms.Label label23;
        public System.Windows.Forms.TextBox Tx_Per_8;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.TextBox Tx_Tem_9;
        private System.Windows.Forms.Label label26;
        public System.Windows.Forms.TextBox Tx_Per_9;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        public System.Windows.Forms.TextBox Tx_Tem_10;
        private System.Windows.Forms.Label label29;
        public System.Windows.Forms.TextBox Tx_Per_10;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
    }
}
