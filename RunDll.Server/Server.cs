using System;
using System.Net;
using System.Net.Sockets;

namespace RunDll
{
    public class Server : IDisposable
    {
        public const int BACKLOG = 100;

        public Server() : this(GetPort())
        {

        }

        public Server(int port)
        {
            this.Endpoint = new IPEndPoint(IPAddress.Loopback, GetPort());
            this.Socket = new Socket(this.Endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.Socket.Listen(BACKLOG);
        }

        public IPEndPoint Endpoint { get; private set; }

        public Socket Socket { get; private set; }

        public void Listen()
        {
            while (this.Socket.Connected)
            {
                var handler = this.Socket.Accept();
                this.Handle(handler);
            }
        }

        protected virtual void Handle(Socket socket)
        {
            var handler = new Handler(this.Endpoint, socket);
            handler.Handle();
            socket.Dispose();
        }

        public void Dispose()
        {
            this.Socket.Close();
        }

        public static int GetPort()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
                return ((IPEndPoint)socket.LocalEndPoint).Port;
            }
        }
    }
}
