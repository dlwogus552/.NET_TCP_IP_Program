namespace MODBUS_TEST_2
{
    partial class Sensor5
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
            this.label26 = new System.Windows.Forms.Label();
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
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("굴림", 18F);
            this.label26.Location = new System.Drawing.Point(34, 9);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(117, 24);
            this.label26.TabIndex = 48;
            this.label26.Text = "전력(kWh)";
            // 
            // Sensor5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(153, 77);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.Tx_Pre_1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Sensor5";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox Tx_Pre_1;
        public System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label26;
    }
}
