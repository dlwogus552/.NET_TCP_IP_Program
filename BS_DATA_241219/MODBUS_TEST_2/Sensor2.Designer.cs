namespace MODBUS_TEST_2
{
    partial class Sensor2
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
            this.Tx_Pre_1 = new System.Windows.Forms.TextBox();
            this.Tx_Pre_2 = new System.Windows.Forms.TextBox();
            this.Tx_Pre_3 = new System.Windows.Forms.TextBox();
            this.Tx_Pre_4 = new System.Windows.Forms.TextBox();
            this.Tx_Pre_5 = new System.Windows.Forms.TextBox();
            this.Tx_Pre_6 = new System.Windows.Forms.TextBox();
            this.Tx_Pre_7 = new System.Windows.Forms.TextBox();
            this.Tx_Pre_8 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
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
            // Tx_Pre_1
            // 
            this.Tx_Pre_1.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Pre_1.Location = new System.Drawing.Point(30, 44);
            this.Tx_Pre_1.Name = "Tx_Pre_1";
            this.Tx_Pre_1.ReadOnly = true;
            this.Tx_Pre_1.Size = new System.Drawing.Size(120, 30);
            this.Tx_Pre_1.TabIndex = 6;
            this.Tx_Pre_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Tx_Pre_2
            // 
            this.Tx_Pre_2.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Pre_2.Location = new System.Drawing.Point(156, 44);
            this.Tx_Pre_2.Name = "Tx_Pre_2";
            this.Tx_Pre_2.ReadOnly = true;
            this.Tx_Pre_2.Size = new System.Drawing.Size(120, 30);
            this.Tx_Pre_2.TabIndex = 7;
            this.Tx_Pre_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Tx_Pre_3
            // 
            this.Tx_Pre_3.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Pre_3.Location = new System.Drawing.Point(282, 44);
            this.Tx_Pre_3.Name = "Tx_Pre_3";
            this.Tx_Pre_3.ReadOnly = true;
            this.Tx_Pre_3.Size = new System.Drawing.Size(120, 30);
            this.Tx_Pre_3.TabIndex = 8;
            this.Tx_Pre_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Tx_Pre_4
            // 
            this.Tx_Pre_4.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Pre_4.Location = new System.Drawing.Point(408, 44);
            this.Tx_Pre_4.Name = "Tx_Pre_4";
            this.Tx_Pre_4.ReadOnly = true;
            this.Tx_Pre_4.Size = new System.Drawing.Size(120, 30);
            this.Tx_Pre_4.TabIndex = 9;
            this.Tx_Pre_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Tx_Pre_5
            // 
            this.Tx_Pre_5.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Pre_5.Location = new System.Drawing.Point(534, 44);
            this.Tx_Pre_5.Name = "Tx_Pre_5";
            this.Tx_Pre_5.ReadOnly = true;
            this.Tx_Pre_5.Size = new System.Drawing.Size(120, 30);
            this.Tx_Pre_5.TabIndex = 10;
            this.Tx_Pre_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Tx_Pre_6
            // 
            this.Tx_Pre_6.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Pre_6.Location = new System.Drawing.Point(660, 44);
            this.Tx_Pre_6.Name = "Tx_Pre_6";
            this.Tx_Pre_6.ReadOnly = true;
            this.Tx_Pre_6.Size = new System.Drawing.Size(120, 30);
            this.Tx_Pre_6.TabIndex = 11;
            this.Tx_Pre_6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Tx_Pre_7
            // 
            this.Tx_Pre_7.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Pre_7.Location = new System.Drawing.Point(786, 44);
            this.Tx_Pre_7.Name = "Tx_Pre_7";
            this.Tx_Pre_7.ReadOnly = true;
            this.Tx_Pre_7.Size = new System.Drawing.Size(120, 30);
            this.Tx_Pre_7.TabIndex = 12;
            this.Tx_Pre_7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Tx_Pre_8
            // 
            this.Tx_Pre_8.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Pre_8.Location = new System.Drawing.Point(912, 44);
            this.Tx_Pre_8.Name = "Tx_Pre_8";
            this.Tx_Pre_8.ReadOnly = true;
            this.Tx_Pre_8.Size = new System.Drawing.Size(120, 30);
            this.Tx_Pre_8.TabIndex = 13;
            this.Tx_Pre_8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("굴림", 20F);
            this.label26.Location = new System.Drawing.Point(27, 9);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(126, 27);
            this.label26.TabIndex = 48;
            this.label26.Text = "압력(bar)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 20F);
            this.label2.Location = new System.Drawing.Point(153, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 27);
            this.label2.TabIndex = 49;
            this.label2.Text = "압력(bar)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 20F);
            this.label3.Location = new System.Drawing.Point(279, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 27);
            this.label3.TabIndex = 50;
            this.label3.Text = "압력(bar)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 20F);
            this.label4.Location = new System.Drawing.Point(405, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 27);
            this.label4.TabIndex = 51;
            this.label4.Text = "압력(bar)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 20F);
            this.label5.Location = new System.Drawing.Point(531, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 27);
            this.label5.TabIndex = 52;
            this.label5.Text = "압력(bar)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 20F);
            this.label6.Location = new System.Drawing.Point(657, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 27);
            this.label6.TabIndex = 53;
            this.label6.Text = "압력(bar)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("굴림", 20F);
            this.label7.Location = new System.Drawing.Point(783, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 27);
            this.label7.TabIndex = 54;
            this.label7.Text = "압력(bar)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("굴림", 20F);
            this.label8.Location = new System.Drawing.Point(909, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 27);
            this.label8.TabIndex = 55;
            this.label8.Text = "압력(bar)";
            // 
            // Sensor2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 77);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.Tx_Pre_8);
            this.Controls.Add(this.Tx_Pre_7);
            this.Controls.Add(this.Tx_Pre_6);
            this.Controls.Add(this.Tx_Pre_5);
            this.Controls.Add(this.Tx_Pre_4);
            this.Controls.Add(this.Tx_Pre_3);
            this.Controls.Add(this.Tx_Pre_2);
            this.Controls.Add(this.Tx_Pre_1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Sensor2";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox Tx_Pre_1;
        public System.Windows.Forms.Timer timer2;
        public System.Windows.Forms.TextBox Tx_Pre_2;
        public System.Windows.Forms.TextBox Tx_Pre_3;
        public System.Windows.Forms.TextBox Tx_Pre_4;
        public System.Windows.Forms.TextBox Tx_Pre_5;
        public System.Windows.Forms.TextBox Tx_Pre_6;
        public System.Windows.Forms.TextBox Tx_Pre_7;
        public System.Windows.Forms.TextBox Tx_Pre_8;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}
