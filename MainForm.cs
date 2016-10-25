using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Sweetjian.MLO2Syncer.Properties;

namespace Sweetjian.MLO2Syncer
{
    public partial class MainForm : Form
    {
        private MLO2Proxy proxy;
        private bool isExit;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;

            //列出本机IP
            string hostName = Dns.GetHostName();  
            foreach (IPAddress ip in Dns.GetHostAddresses(hostName))
            {
                if (ip.AddressFamily == AddressFamily.InterNetworkV6) continue;

                cfgListenIP.Items.Add(ip.ToString());
            }

            Debug.Listeners.Add(new TextBoxListener(tbMsg) {Name = "TextBoxListener"});

            LoadConfig();

            if (string.IsNullOrWhiteSpace(Settings.Default.MloDataFile) == false
                && File.Exists(Settings.Default.MloDataFile))
            {
                btnStart.PerformClick();
            }

            base.OnLoad(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            Debug.Listeners.Remove("TextBoxListener");
            base.OnClosed(e);
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (isExit == false)
            {
                e.Cancel = true;
                this.Hide();
            }
            base.OnClosing(e);
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            if (proxy.IsRunning)
            {
                proxy.Stop();
            }
            this.isExit = true;
            this.Close();
        }
        private void OnShowClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
            }
            else
            {
                contextMenuStrip.Show(MousePosition.X, MousePosition.Y);
            }
        }
        private void OnStartStop(object sender, EventArgs e)
        {
            if (proxy == null || proxy.IsRunning == false)
            {
                SaveConfig();

                proxy = new MLO2Proxy(Settings.Default.ListenIP,
                                  Settings.Default.ListenPort,
                                  Settings.Default.MloDataFile,
                                  Settings.Default.DelayKillMlo);
                try
                {
                    proxy.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "启动失败，请检查配置", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Trace.TraceInformation(ex.ToString());
                    return;
                }
                
                
                DoRefreshUI(true);
            }
            else
            {
                proxy.Stop();
                DoRefreshUI(false);
            }
        }

        private void DoRefreshUI(bool isStart)
        {
            foreach (Control c in panelConfig.Controls)
            {
                c.Enabled = !isStart;
            }
            btnStart.Enabled = true;
            btnStart.Text = isStart 
                ? "运行中..\r\n\r\n点我停止" 
                : "已停止..\r\n\r\n点我启动";
        }

        private void LoadConfig()
        {
            cfgListenIP.Text = Settings.Default.ListenIP;
            cfgListenPort.Value = Settings.Default.ListenPort;
            cfgMloDataFile.Text = Settings.Default.MloDataFile;
            cfgIdleSec.Value = Convert.ToDecimal(Settings.Default.DelayKillMlo / 1000);
        }
        private void SaveConfig()
        {
            Settings.Default.ListenIP = cfgListenIP.Text; 
            Settings.Default.ListenPort = (int)cfgListenPort.Value;
            Settings.Default.MloDataFile = cfgMloDataFile.Text;
            Settings.Default.DelayKillMlo = Convert.ToInt32(cfgIdleSec.Value*1000);
            Settings.Default.Save();
        }

        private void OnMlFileSelectClick(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = ".ml 文件|*.ml|所有文件|*.*";
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                cfgMloDataFile.Text = dialog.FileName;
            }
        }
    }

    public class TextBoxListener : TraceListener
    {
        private TextBox tbMsg;

        public TextBoxListener(TextBox tbMsg)
        {
            this.tbMsg = tbMsg;
        }

        #region Overrides of TraceListener

        public override void Write(string message)
        {
            if (tbMsg.IsHandleCreated && tbMsg.IsDisposed == false)
            {
                tbMsg.BeginInvoke((MethodInvoker)(() =>
                {
                    tbMsg.AppendText(
                        string.Format("{0:yyyy-MM-dd HH:mm:ss} {1}{2}",
                                      DateTime.Now,
                                      message,
                                      Environment.NewLine));
                    if (tbMsg.Text.Length > 4096)
                    {
                        tbMsg.Text = tbMsg.Text.Substring(tbMsg.Text.Length / 2);
                    }
                }));
            }
        }

        public override void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }

        #endregion
    }
}
