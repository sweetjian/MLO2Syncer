using System;
using System.Threading;
using System.Windows.Forms;

namespace Sweetjian.MLO2Syncer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Application.ThreadException += OnThreadException;

            bool createNew;
            using (new Mutex(true, "MLO2Syncer", out createNew))
            {
                if (createNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
                else
                {
                    MessageBox.Show("MLO2Syncer 应用程序已经在运行中...");
                    Environment.Exit(1);
                }
            }
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowException(e.ExceptionObject as Exception, true);
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowException(e.Exception, true);
        }

        private static void ShowException(Exception error, bool exit)
        {
            MessageBox.Show(string.Format("{0}", error),
                            "MLO2Syncer 程序异常，即将退出",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            Environment.Exit(-1);
        }
    }
}
