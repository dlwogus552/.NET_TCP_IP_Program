namespace MODBUS_TEST_2
{
    partial class Sensor3
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
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.Tx_Ele_2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Tx_Ele_3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Tx_Ele_4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Tx_Ele_5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Tx_Ele_6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Tx_Ele_7 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Tx_Ele_1 = new System.Windows.Forms.TextBox();
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
            // timer2
            // 
            this.timer2.Interval = 60000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Tx_Ele_2
            // 
            this.Tx_Ele_2.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Ele_2.Location = new System.Drawing.Point(129, 44);
            this.Tx_Ele_2.Name = "Tx_Ele_2";
            this.Tx_Ele_2.ReadOnly = true;
            this.Tx_Ele_2.Size = new System.Drawing.Size(120, 30);
            this.Tx_Ele_2.TabIndex = 8;
            this.Tx_Ele_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 18F);
            this.label3.Location = new System.Drawing.Point(130, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "전력(kWh)";
            // 
            // Tx_Ele_3
            // 
            this.Tx_Ele_3.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Ele_3.Location = new System.Drawing.Point(255, 44);
            this.Tx_Ele_3.Name = "Tx_Ele_3";
            this.Tx_Ele_3.ReadOnly = true;
            this.Tx_Ele_3.Size = new System.Drawing.Size(120, 30);
            this.Tx_Ele_3.TabIndex = 10;
            this.Tx_Ele_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 18F);
            this.label4.Location = new System.Drawing.Point(256, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "전력(kWh)";
            // 
            // Tx_Ele_4
            // 
            this.Tx_Ele_4.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Ele_4.Location = new System.Drawing.Point(381, 44);
            this.Tx_Ele_4.Name = "Tx_Ele_4";
            this.Tx_Ele_4.ReadOnly = true;
            this.Tx_Ele_4.Size = new System.Drawing.Size(120, 30);
            this.Tx_Ele_4.TabIndex = 12;
            this.Tx_Ele_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 18F);
            this.label5.Location = new System.Drawing.Point(382, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "전력(kWh)";
            // 
            // Tx_Ele_5
            // 
            this.Tx_Ele_5.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Ele_5.Location = new System.Drawing.Point(507, 44);
            this.Tx_Ele_5.Name = "Tx_Ele_5";
            this.Tx_Ele_5.ReadOnly = true;
            this.Tx_Ele_5.Size = new System.Drawing.Size(120, 30);
            this.Tx_Ele_5.TabIndex = 14;
            this.Tx_Ele_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 18F);
            this.label6.Location = new System.Drawing.Point(508, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "전력(kWh)";
            // 
            // Tx_Ele_6
            // 
            this.Tx_Ele_6.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Ele_6.Location = new System.Drawing.Point(633, 44);
            this.Tx_Ele_6.Name = "Tx_Ele_6";
            this.Tx_Ele_6.ReadOnly = true;
            this.Tx_Ele_6.Size = new System.Drawing.Size(120, 30);
            this.Tx_Ele_6.TabIndex = 16;
            this.Tx_Ele_6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("굴림", 18F);
            this.label7.Location = new System.Drawing.Point(634, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 24);
            this.label7.TabIndex = 15;
            this.label7.Text = "전력(kWh)";
            // 
            // Tx_Ele_7
            // 
            this.Tx_Ele_7.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Ele_7.Location = new System.Drawing.Point(759, 44);
            this.Tx_Ele_7.Name = "Tx_Ele_7";
            this.Tx_Ele_7.ReadOnly = true;
            this.Tx_Ele_7.Size = new System.Drawing.Size(120, 30);
            this.Tx_Ele_7.TabIndex = 18;
            this.Tx_Ele_7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("굴림", 18F);
            this.label8.Location = new System.Drawing.Point(760, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 24);
            this.label8.TabIndex = 17;
            this.label8.Text = "전력(kWh)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 18F);
            this.label2.Location = new System.Drawing.Point(4, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "전력(kWh)";
            // 
            // Tx_Ele_1
            // 
            this.Tx_Ele_1.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Ele_1.Location = new System.Drawing.Point(3, 44);
            this.Tx_Ele_1.Name = "Tx_Ele_1";
            this.Tx_Ele_1.ReadOnly = true;
            this.Tx_Ele_1.Size = new System.Drawing.Size(120, 30);
            this.Tx_Ele_1.TabIndex = 6;
            this.Tx_Ele_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Sensor3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 78);
            this.Controls.Add(this.Tx_Ele_7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Tx_Ele_6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Tx_Ele_5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Tx_Ele_4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Tx_Ele_3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Tx_Ele_2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Tx_Ele_1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Sensor3";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Timer timer2;
        public System.Windows.Forms.TextBox Tx_Ele_2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox Tx_Ele_3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox Tx_Ele_4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox Tx_Ele_5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox Tx_Ele_6;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox Tx_Ele_7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox Tx_Ele_1;
    }
}
