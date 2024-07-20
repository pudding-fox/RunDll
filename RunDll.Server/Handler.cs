using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace RunDll
{
    public class Handler
    {
        public const int BUFFER_SIZE = 1024;

        public Handler(IPEndPoint endpoint, Socket socket)
        {
            this.Endpoint = endpoint;
            this.Socket = socket;
        }

        public IPEndPoint Endpoint { get; private set; }

        public Socket Socket { get; private set; }

        public void Handle()
        {
            var buffer = new byte[BUFFER_SIZE];
            var count = this.Socket.Receive(buffer);
            using (var input = new MemoryStream(buffer))
            {
                var formatter = new BinaryFormatter();
                var request = (RunRequest)formatter.Deserialize(input);
                var response = this.Handle(request);
                using (var output = new MemoryStream())
                {
                    formatter.Serialize(output, response);
                    output.Seek(0, SeekOrigin.Begin);
                    this.Socket.Send(output.ToArray());
                }
            }
        }

        public RunResponse Handle(RunRequest request)
        {
            var assembly = Assembly.LoadFile(request.Location);
            var type = assembly.GetType(request.Type);
            var method = type.GetMethod(request.Method);
            var result = method.Invoke(assembly, request.Arguments);
            return new RunResponse(result);
        }
    }
}
