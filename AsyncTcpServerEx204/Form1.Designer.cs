namespace AsyncTcpServer
{
    partial class FrmServer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmServer));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSendMes = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtport = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtaddress = new System.Windows.Forms.TextBox();
            this.txtBoxMessag = new System.Windows.Forms.TextBox();
            this.rtxtboxMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(195, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始监听";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(310, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "结束监听";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 570);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "发送地址:端口";
            // 
            // btnSendMes
            // 
            this.btnSendMes.Location = new System.Drawing.Point(373, 564);
            this.btnSendMes.Name = "btnSendMes";
            this.btnSendMes.Size = new System.Drawing.Size(75, 23);
            this.btnSendMes.TabIndex = 6;
            this.btnSendMes.Text = "发送信息";
            this.btnSendMes.UseVisualStyleBackColor = true;
            this.btnSendMes.Click += new System.EventHandler(this.btnSendMes_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(416, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtport
            // 
            this.txtport.Location = new System.Drawing.Point(89, 13);
            this.txtport.Name = "txtport";
            this.txtport.Size = new System.Drawing.Size(72, 21);
            this.txtport.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(39, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "端口：";
            // 
            // txtaddress
            // 
            this.txtaddress.Location = new System.Drawing.Point(121, 565);
            this.txtaddress.Name = "txtaddress";
            this.txtaddress.Size = new System.Drawing.Size(202, 21);
            this.txtaddress.TabIndex = 11;
            // 
            // txtBoxMessag
            // 
            this.txtBoxMessag.Location = new System.Drawing.Point(21, 427);
            this.txtBoxMessag.Multiline = true;
            this.txtBoxMessag.Name = "txtBoxMessag";
            this.txtBoxMessag.Size = new System.Drawing.Size(470, 131);
            this.txtBoxMessag.TabIndex = 12;
            // 
            // rtxtboxMessage
            // 
            this.rtxtboxMessage.Location = new System.Drawing.Point(21, 41);
            this.rtxtboxMessage.Multiline = true;
            this.rtxtboxMessage.Name = "rtxtboxMessage";
            this.rtxtboxMessage.Size = new System.Drawing.Size(470, 374);
            this.rtxtboxMessage.TabIndex = 13;
            // 
            // FrmServer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(526, 597);
            this.Controls.Add(this.rtxtboxMessage);
            this.Controls.Add(this.txtBoxMessag);
            this.Controls.Add(this.txtaddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtport);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSendMes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmServer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "API Server";
            this.Load += new System.EventHandler(this.FrmServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSendMes;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtaddress;
        private System.Windows.Forms.TextBox txtBoxMessag;
        private System.Windows.Forms.TextBox rtxtboxMessage;
    }
}

