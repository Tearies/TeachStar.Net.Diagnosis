using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace TeachStar.Net.Diagnosis.Common.Net
{
    internal class NetDiagnosis : IDisposable
    {
        private Ping _localPing;
        private AutoResetEvent waiter;
        private int _timeOut;
        private PingOptions _options;
        private int maxHops;
        private IPAddress _destination;
        private byte[] _buffer;
        private static readonly  object lockerobj=new object();

        private NetDiagnosis()
        {
            _nodes = new List<ICMPNode>();
            _localPing = new Ping();
            waiter = new AutoResetEvent(false);
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public NetDiagnosis(IPAddress destination, int timeOut = 3000, int maxHops = 2) : this()
        {
            _timeOut = timeOut;
            this.maxHops = maxHops;
            _destination = destination;
        }

        public void Tracert()
        {
            ThreadPool.UnsafeQueueUserWorkItem((p) =>
            {
                InternalTracert();
            }, null);
        }

        public void Waite()
        {
            waiter.WaitOne();
        }

        internal bool IsDone
        {
            get
            {
                return _isDone;
            }
            private set
            {
                _isDone = value;
            }
        }

        private byte[] Buffer
        {
            get
            {
                if (_buffer == null)
                {
                    _buffer = new byte[32];
                    for (int i = 0; i < Buffer.Length; i++)
                    {
                        _buffer[i] = 0x65;
                    }
                }
                return _buffer;
            }
        }

        private List<ICMPNode> _nodes;
        private bool _isDone;

        public ICMPNode RouteNode
        {
            get
            {
                lock (_nodes)
                {
                    if (_nodes.Any())
                    {
                        return _nodes.First();
                    }
                }
                return new ICMPNode(_destination, 0, IPStatus.BadDestination);
            }
        }
        private void InternalTracert()
        {
            try
            {
                if (IPAddress.IsLoopback(_destination))
                {
                    ProcessNode(_destination, IPStatus.Success);
                }
                else
                {
                    _localPing = new Ping();
                    _localPing.PingCompleted += OnPingCompleted;
                    _options = new PingOptions(1, true);
                    _localPing.SendAsync(_destination, _timeOut, Buffer, _options, null);
                }
            }
            catch
            {
                waiter.Set();
            }
        }

        private void OnPingCompleted(object sender, PingCompletedEventArgs e)
        {
            ProcessNode(e.Reply.Address, e.Reply.Status);
            _options.Ttl += 1;

            if (!this.IsDone)
            {
                lock (lockerobj)
                {
                    if (_localPing == null)
                    {
                        ProcessNode(_destination, IPStatus.Unknown);
                    }
                    else
                    {
                        _localPing.SendAsync(_destination, _timeOut, Buffer, _options, null);
                    }
                }
            }
        }

        private void ProcessNode(IPAddress replyAddress, IPStatus replyStatus)
        {
            long roundTripTime = 0;

            if (replyStatus == IPStatus.TtlExpired || replyStatus == IPStatus.Success)
            {
                Ping pingIntermediate = new Ping();

                try
                {
                    PingReply reply = pingIntermediate.Send(replyAddress, _timeOut);
                    roundTripTime = reply.RoundtripTime;
                    replyStatus = reply.Status;
                }
                catch (PingException e)
                {

                }
                finally
                {
                    pingIntermediate.Dispose();
                }
            }
            ICMPNode node = new ICMPNode(replyAddress, roundTripTime, replyStatus);

            lock (_nodes)
            {
                _nodes.Add(node);
            }
            var isDone = replyAddress.Equals(_destination);
            this.IsDone = isDone;
            if (isDone)
            {
                waiter.Set();
            }
            lock (_nodes)
            {
                if (!this.IsDone && _nodes.Count >= maxHops - 1)
                    ProcessNode(_destination, IPStatus.Success);
            }
        }


        #region IDisposable

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {

            _localPing?.Dispose();
            waiter?.Dispose();
        }

        #endregion

        public bool InSameVLan(string argIp)
        {
            var target = IPAddress.Parse(argIp).GetAddressBytes();
            var current = RouteNode.Address.GetAddressBytes();
            return target[0] == current[0] && target[1] == current[1] &&
                   target[2] == current[2];
        }
    }
}