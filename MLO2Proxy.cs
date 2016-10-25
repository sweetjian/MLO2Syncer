using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Sweetjian.MLO2Syncer
{
    /// <summary>
    /// 通过代理MLO软件WIFI同步数据实现控制打开或关闭“.ml”文件
    /// </summary>
    public class MLO2Proxy
    {
        private const int MLO2_WIFI_SYNC_PORT = 21030;

        private readonly object mloExeSync = new object();
        private readonly Timer tmDelayKillMlo;
        private readonly string mloDataFile;

        private int delayKillMlo = 15 * 1000;
        private string listenIp;
        private int listenPort;
        private TcpListener listener;
        private Process mloProcess;
        private bool isMloExeRun;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listenIp">本机监听IP</param>
        /// <param name="listenPort">监听端口，非21030端口，转发至21030。默认21031</param>
        /// <param name="mloDataFile"></param>
        /// <param name="delayKillMlo"> </param>
        public MLO2Proxy(string listenIp, int listenPort, string mloDataFile,int delayKillMlo)
        {
            this.listenIp     = listenIp;
            this.listenPort   = listenPort;
            this.mloDataFile  = mloDataFile;
            this.delayKillMlo = delayKillMlo;

            listener = new TcpListener(IPAddress.Parse(listenIp), listenPort);
            tmDelayKillMlo = new Timer(CloseMloTick);
        }

        public bool IsRunning { get; private set; }
        public bool StopPending { get; private set; }

        public void Start()
        {
            if(IsRunning) return;

            if (string.IsNullOrWhiteSpace(mloDataFile) || File.Exists(mloDataFile) == false)
            {
                throw new FileNotFoundException(".ml数据文件没有正确配置");
            }

            listener.Start();
            listener.BeginAcceptTcpClient(OnAcceptClient, null);
            IsRunning = true;
            StopPending = false;
            Trace.TraceInformation("启动MLO2代理。listen:{0}:{1} data:{2}", listenIp, listenPort, mloDataFile);
        }
        public void Stop()
        {
            if(IsRunning)
            {
                StopPending = true;
                try
                {
                    listener.Stop();
                }
                catch (SocketException)
                {
                    
                }
                Trace.TraceInformation("停止MLO2代理");
            }
        }

        private void OnAcceptClient(IAsyncResult ar)
        {
            if (StopPending)
            {
                try
                {
                    listener.EndAcceptTcpClient(ar);
                }
                catch
                {
                }
                IsRunning = false;
                StopPending = false;
                return;
            }

            TcpClient server = null;
            try
            {
                StartMLO2App();
                server = new TcpClient();
                server.Connect(listenIp, MLO2_WIFI_SYNC_PORT);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Failed to run MLO2 exe. " + ex.Message);
            }

            TcpClient client = null;
            try
            {
                client = listener.EndAcceptTcpClient(ar);
                Trace.TraceInformation("Accept client: {0}", client.Client.RemoteEndPoint);
            }
            catch (Exception ex)
            {
                DelayCloseMl();
                Trace.TraceError("Connect failed. " + ex.Message);
            }
            if (server != null
                && server.Connected
                && client != null
                && client.Connected)
            {
                Context ctxC2S = new Context(client, server);
                Context ctxS2C = new Context(server, client);

                try
                {
                    BeginReadDat(ctxC2S);
                }
                catch (Exception ex)
                {
                    OnClientLogout(ctxC2S, ex);
                }
                try
                {
                    BeginReadDat(ctxS2C);
                }
                catch (Exception ex)
                {
                    OnClientLogout(ctxS2C, ex);
                }
            }
            else
            {
                if (server != null && server.Connected)
                {
                    server.Close();
                }
                if (client != null && client.Connected)
                {
                    client.Close();
                }
            }

            if (StopPending)
            {
                IsRunning = false;
                return;
            }
            try
            {
                listener.BeginAcceptTcpClient(OnAcceptClient, null);
            }
            catch (Exception ex)
            {
                IsRunning = false;
                if (StopPending)
                {
                    StopPending = false;
                    return;
                }
                Trace.TraceError("Failed to AcceptTcpClient. {0}", ex.Message);
                throw;
            }
        }
        private void OnClientLogout(Context ctx, Exception ex)
        {
            if (ex != null && (ex is ObjectDisposedException == false))
            {
                Trace.TraceError("Proxy error: {0}", ex.Message);
            }
            Trace.TraceInformation("连接断开 " + ctx.Description);
            if (ctx.Source.Client != null
                && ctx.Source.Client.Connected)
            {
                try { ctx.Source.Close(); } catch { }
            }

            if (ctx.Target.Client != null
                && ctx.Target.Client.Connected)
            {
                try { ctx.Target.Close(); } catch { }
            }

            DelayCloseMl();
        }

        private void BeginReadDat(Context ctx)
        {
            ctx.Source.GetStream().BeginRead(
                   ctx.Buffer,
                   0,
                   ctx.Buffer.Length,
                   OnDataReaded,
                   ctx);
        }
        private void OnDataReaded(IAsyncResult ar)
        {
            DelayCloseMl();

            Context ctx = (Context) ar.AsyncState;

            try
            {
                int count = ctx.Source.GetStream().EndRead(ar);

                if(count==0)
                {
                    OnClientLogout(ctx, null);
                    return;
                }
                Trace.TraceInformation("{0}=>{1}: {2}",
                                       ctx.Source.Client.LocalEndPoint,
                                       ctx.Target.Client.RemoteEndPoint,
                                       Encoding.ASCII.GetString(ctx.Buffer, 0, count));

                ctx.Target.GetStream().Write(ctx.Buffer, 0, count);

                if (ctx.Source.Client != null && ctx.Source.Client.Connected)
                {
                    BeginReadDat(ctx);
                }
            }
            catch(Exception ex)
            {
                OnClientLogout(ctx, ex);
            }
        }

        private void StartMLO2App()
        {
            lock (mloExeSync)
            {
                tmDelayKillMlo.Change(-1, -1);

                if (isMloExeRun == false)
                {
                    mloProcess = Process.Start(mloDataFile);
                    if (mloProcess == null)
                    {
                        throw new Exception("无法启动MLO2程序");
                    }
                    mloProcess.WaitForInputIdle();
                    isMloExeRun = true;
                }
                DelayCloseMl();
            }
        }
        private void DelayCloseMl()
        {
            tmDelayKillMlo.Change(delayKillMlo, -1);
        }
        private void CloseMloTick(object state)
        {
            lock (mloExeSync)
            {
                if (mloProcess != null)
                {
                    try
                    {
                        if (mloProcess.HasExited == false)
                        {
                            mloProcess.CloseMainWindow();
                            bool isExit = mloProcess.WaitForExit(3000);
                            mloProcess = null;
                            Trace.TraceInformation("MLO2进程关闭" + (isExit ? "成功" : "失败"));
                        }
                        else
                        {
                            Trace.TraceInformation("MLO2进程已处于关闭状态");
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError("无法关闭进程: " + ex.Message);
                    }
                }
                var processes = Process.GetProcessesByName("mlo");
                if (processes.Length > 0)
                {
                    foreach (var process in processes)
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError("Failed to kill MLO process. {0}", ex.Message);
                        }
                    }
                }

                mloProcess = null;
                isMloExeRun = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        class Context
        {
            public Context(TcpClient source, TcpClient target)
            {
                this.Source = source;
                this.Target = target;
                this.Description = source.Client.LocalEndPoint + "=>" + source.Client.RemoteEndPoint;
            }
            public readonly string Description;
            public readonly TcpClient Source;
            public readonly TcpClient Target;
            public readonly byte[] Buffer = new byte[1024];
        }
    }
}
