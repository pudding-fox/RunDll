using System.IO;
using System.Net.Sockets;

namespace RunDll
{
    public static partial class Extensions
    {
        const int BUFFER_SIZE = 1024;

        public static byte[] ReceiveAll(this Socket socket)
        {
            using (var stream = new MemoryStream())
            {
                var buffer = new byte[BUFFER_SIZE];
                do
                {
                    var count = socket.Receive(buffer);
                    stream.Write(buffer, 0, count);
                } while (socket.Available > 0);
                return stream.ToArray();
            }
        }
    }
}
