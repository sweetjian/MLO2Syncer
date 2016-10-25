namespace Sweetjian.MLO2Syncer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tbMsg = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelConfig = new System.Windows.Forms.Panel();
            this.btnMlFile = new System.Windows.Forms.Button();
            this.cfgMloDataFile = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.cfgListenIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cfgIdleSec = new System.Windows.Forms.NumericUpDown();
            this.cfgListenPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStrip.SuspendLayout();
            this.panelConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgIdleSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfgListenPort)).BeginInit();
            this.SuspendLayout();
            // 
            // tbMsg
            // 
            this.tbMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMsg.Location = new System.Drawing.Point(0, 85);
            this.tbMsg.Multiline = true;
            this.tbMsg.Name = "tbMsg";
            this.tbMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMsg.Size = new System.Drawing.Size(459, 257);
            this.tbMsg.TabIndex = 0;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "MLO2 手机WIFI数据同步工具";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnShowClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.OnExitClick);
            // 
            // panelConfig
            // 
            this.panelConfig.Controls.Add(this.btnMlFile);
            this.panelConfig.Controls.Add(this.cfgMloDataFile);
            this.panelConfig.Controls.Add(this.btnStart);
            this.panelConfig.Controls.Add(this.cfgListenIP);
            this.panelConfig.Controls.Add(this.label3);
            this.panelConfig.Controls.Add(this.label2);
            this.panelConfig.Controls.Add(this.cfgIdleSec);
            this.panelConfig.Controls.Add(this.cfgListenPort);
            this.panelConfig.Controls.Add(this.label1);
            this.panelConfig.Controls.Add(this.label4);
            this.panelConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelConfig.Location = new System.Drawing.Point(0, 0);
            this.panelConfig.Name = "panelConfig";
            this.panelConfig.Size = new System.Drawing.Size(459, 85);
            this.panelConfig.TabIndex = 1;
            // 
            // btnMlFile
            // 
            this.btnMlFile.Location = new System.Drawing.Point(330, 33);
            this.btnMlFile.Name = "btnMlFile";
            this.btnMlFile.Size = new System.Drawing.Size(25, 24);
            this.btnMlFile.TabIndex = 4;
            this.btnMlFile.Text = "...";
            this.btnMlFile.UseVisualStyleBackColor = true;
            this.btnMlFile.Click += new System.EventHandler(this.OnMlFileSelectClick);
            // 
            // cfgMloDataFile
            // 
            this.cfgMloDataFile.Location = new System.Drawing.Point(97, 35);
            this.cfgMloDataFile.Name = "cfgMloDataFile";
            this.cfgMloDataFile.Size = new System.Drawing.Size(236, 21);
            this.cfgMloDataFile.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(363, 6);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(86, 73);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "已停止..\r\n\r\n点我启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.OnStartStop);
            // 
            // cfgListenIP
            // 
            this.cfgListenIP.Location = new System.Drawing.Point(97, 9);
            this.cfgListenIP.Name = "cfgListenIP";
            this.cfgListenIP.Size = new System.Drawing.Size(165, 21);
            this.cfgListenIP.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "空闲";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = ".ml文件位置：";
            // 
            // cfgIdleSec
            // 
            this.cfgIdleSec.Location = new System.Drawing.Point(97, 58);
            this.cfgIdleSec.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.cfgIdleSec.Name = "cfgIdleSec";
            this.cfgIdleSec.Size = new System.Drawing.Size(43, 21);
            this.cfgIdleSec.TabIndex = 2;
            this.cfgIdleSec.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // cfgListenPort
            // 
            this.cfgListenPort.Location = new System.Drawing.Point(268, 8);
            this.cfgListenPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.cfgListenPort.Name = "cfgListenPort";
            this.cfgListenPort.Size = new System.Drawing.Size(87, 21);
            this.cfgListenPort.TabIndex = 2;
            this.cfgListenPort.Value = new decimal(new int[] {
            21031,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "监听IP、端口：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(139, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "秒关闭MLO，第一次匹配的时候设久一点";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 342);
            this.Controls.Add(this.tbMsg);
            this.Controls.Add(this.panelConfig);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MLO2 手机WIFI数据同步工具";
            this.contextMenuStrip.ResumeLayout(false);
            this.panelConfig.ResumeLayout(false);
            this.panelConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgIdleSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfgListenPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMsg;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.Panel panelConfig;
        private System.Windows.Forms.TextBox cfgListenIP;
        private System.Windows.Forms.NumericUpDown cfgListenPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox cfgMloDataFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown cfgIdleSec;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnMlFile;

    }
}

