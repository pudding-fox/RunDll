using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace RunDll
{
    public abstract class Runner : IDisposable
    {
        protected Runner()
        {
            var fileName = typeof(Program).Assembly.Location;
            this.Info = new ProcessStartInfo(fileName)
            {
                WorkingDirectory = Path.GetDirectoryName(fileName),
                RedirectStandardOutput = true
            };
            this.Process = Process.Start(this.Info);
        }

        public ProcessStartInfo Info { get; private set; }

        public Process Process { get; private set; }

        public object Run(MethodInfo method, Type[] genericArguments, object[] arguments)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.Process.Kill();
            this.Process.WaitForExit();
        }

        public class NetCore : Runner
        {

        }

        public class NetFramework : Runner
        {

        }
    }
}
