namespace MODBUS_TEST_2
{
    partial class Sensor4
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
            this.label26 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.Tx_Pre_1.Location = new System.Drawing.Point(4, 44);
            this.Tx_Pre_1.Name = "Tx_Pre_1";
            this.Tx_Pre_1.ReadOnly = true;
            this.Tx_Pre_1.Size = new System.Drawing.Size(120, 30);
            this.Tx_Pre_1.TabIndex = 6;
            this.Tx_Pre_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Tx_Pre_2
            // 
            this.Tx_Pre_2.Font = new System.Drawing.Font("굴림", 15F);
            this.Tx_Pre_2.Location = new System.Drawing.Point(130, 44);
            this.Tx_Pre_2.Name = "Tx_Pre_2";
            this.Tx_Pre_2.ReadOnly = true;
            this.Tx_Pre_2.Size = new System.Drawing.Size(120, 30);
            this.Tx_Pre_2.TabIndex = 7;
            this.Tx_Pre_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("굴림", 20F);
            this.label26.Location = new System.Drawing.Point(1, 9);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(126, 27);
            this.label26.TabIndex = 48;
            this.label26.Text = "압력(bar)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 20F);
            this.label2.Location = new System.Drawing.Point(127, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 27);
            this.label2.TabIndex = 49;
            this.label2.Text = "압력(bar)";
            // 
            // Sensor4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 77);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.Tx_Pre_2);
            this.Controls.Add(this.Tx_Pre_1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Sensor4";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox Tx_Pre_1;
        public System.Windows.Forms.Timer timer2;
        public System.Windows.Forms.TextBox Tx_Pre_2;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label2;
    }
}
