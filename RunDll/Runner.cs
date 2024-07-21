﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace RunDll
{
    public class Runner : IDisposable
    {
        public Runner(Runtime runtime)
        {
            var directoryName = default(string);
            switch (runtime)
            {
                case Runtime.NetCore:
                    directoryName = "net6.0";
                    break;
                case Runtime.NetFramework:
                    directoryName = "net48";
                    break;
                default:
                    throw new NotImplementedException();
            }
            var fileName = Path.Combine(
                Path.GetDirectoryName(typeof(Runner).Assembly.Location),
                directoryName,
                "RunDll.Server.exe"
            );
            this.Info = new ProcessStartInfo(fileName)
            {
                WorkingDirectory = directoryName,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            this.Process = Process.Start(this.Info);
            using (var reader = this.Process.StandardOutput)
            {
                var line = reader.ReadLine();
                this.Endpoint = new IPEndPoint(IPAddress.Loopback, int.Parse(line));
            }
        }

        public ProcessStartInfo Info { get; private set; }

        public Process Process { get; private set; }

        public IPEndPoint Endpoint { get; private set; }

        public object Run(string assembly, string type, string method, object[] arguments)
        {
            var request = new RunRequest(assembly, type, method, arguments);
            using (var socket = new Socket(this.Endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(this.Endpoint);
                socket.Send(Serializer.Serialize(request));
                var response = Serializer.Deserialize<RunResponse>(socket.ReceiveAll());
                return response.Result;
            }
        }

        public void Dispose()
        {
            this.Process.Kill();
            this.Process.WaitForExit();
        }
    }
}