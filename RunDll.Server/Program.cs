using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunDll
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var server = new Server();
            Console.WriteLine(server.Endpoint.Port);
        }
    }
}
