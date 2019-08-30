using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TeachStar.Net.Diagnosis.Common.Net
{
    public class SocketListener
    {
        private TcpListener _tcpListener;
        private CancellationTokenSource cancellationTokenSource;
        private byte[] buffer;
        private Thread listener;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public SocketListener()
        {

            buffer = new byte[1024];
        }

        #region Implementation of IDisposable

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Stop();
            buffer = null;
        }

        #endregion

        #region Implementation of ISokectListerner

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="localaddr"></param>
        /// <param name="port"></param>
        public void StartListener(IPAddress localaddr, int port)
        {
            Stop();
            cancellationTokenSource = new CancellationTokenSource();
            _tcpListener = new TcpListener(localaddr, port);
            Console.WriteLine($"Liston at tcp://{localaddr}:{port}");
            _tcpListener.ExclusiveAddressUse = true;
            _tcpListener.Start();
            listener = new Thread(() =>
                {
                    while (!cancellationTokenSource.IsCancellationRequested)
                    {
                        InternalAcceptTcpClient();
                    }
                })
            { IsBackground = true };
            listener.SetApartmentState(ApartmentState.STA);
            listener.Start();

        }

        private void InternalAcceptTcpClient()
        {
            try
            {
                var tcpClient = _tcpListener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem((p) =>
                {
                    while (true)
                    {
                        try
                        {
                            var tempTcpClient = p as TcpClient;
                            var remoteAddress = tempTcpClient.Client.RemoteEndPoint as IPEndPoint;
                            var tmpbuffer = new List<byte>();
                            do
                            {
                                var readcount = tempTcpClient.Client.Receive(buffer);
                                tmpbuffer.AddRange(buffer.Take(readcount));
                            } while (tempTcpClient.Client.Available > 0);
                            var str = Encoding.UTF8.GetString(tmpbuffer.ToArray());
                            OnMessageReceivedEvent(new SocketEventArgs(remoteAddress.Address,remoteAddress.Port, str));
                        }
                        catch (Exception ex)
                        {
                            OnErrorEvent(new ErrorEventArgs(ex));
                            break;
                        }
                    }

                }, tcpClient);

            }
            catch (ThreadAbortException e)
            {

            }
            catch (Exception e)
            {
                OnErrorEvent(new ErrorEventArgs(e));
            }
        }

        /// <summary>
        /// 停止监听
        /// </summary>
        public void Stop()
        {
            listener?.Abort();
            _tcpListener?.Stop();
            cancellationTokenSource?.Cancel(false);
            cancellationTokenSource?.Dispose();

        }

        /// <summary>
        /// 收到新的TCP信息
        /// </summary>
        public event EventHandler<SocketEventArgs> MessageReceivedEvent;
        public event EventHandler<ErrorEventArgs> ErrorEvent;
        #endregion

        protected virtual void OnMessageReceivedEvent(SocketEventArgs e)
        {
            MessageReceivedEvent?.Invoke(this, e);
        }

        protected virtual void OnErrorEvent(ErrorEventArgs e)
        {
            ErrorEvent?.Invoke(this, e);
        }
    }
}