using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace RunDll
{
    public class Handler
    {
        public Handler(IPEndPoint endpoint, Socket socket)
        {
            this.Endpoint = endpoint;
            this.Socket = socket;
        }

        public IPEndPoint Endpoint { get; private set; }

        public Socket Socket { get; private set; }

        public void Handle()
        {
            var request = Serializer.Deserialize<RunRequest>(this.Socket.ReceiveAll());
            var response = this.Handle(request);
            this.Socket.Send(Serializer.Serialize(response));
        }

        public RunResponse Handle(RunRequest request)
        {
            var assembly = Assembly.LoadFrom(request.Assembly);
            var type = assembly.GetType(request.Type);
            var instance = Activator.CreateInstance(type);
            var method = type.GetMethod(request.Method);
            var result = method.Invoke(instance, request.Arguments);
            return new RunResponse(result);
        }
    }
}
