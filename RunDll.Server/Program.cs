using System;
using System.Diagnostics;

namespace RunDll
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var server = new Server();
            Debugger.Launch();
            Console.WriteLine(server.Endpoint.Port);
            server.Listen();
        }
    }
}
