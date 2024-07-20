using System.Net.Sockets;
using System.Net;

namespace RunDll
{
    public class Client<T> : IServer<T>
    {
        public Client(Runtime runtime)
        {
            var target = default(T);
            this.Interceptor = Proxy.Create<T>(out target);
            this.Interceptor.Intercepted += this.OnIntercepted;
            this.Target = target;
            switch (runtime)
            {
                case Runtime.NetCore:
                    this.Runner = new Runner.NetCore();
                    break;
                case Runtime.NetFramework:
                    this.Runner = new Runner.NetFramework();
                    break;
            }
        }

        public Proxy.Interceptor Interceptor { get; private set; }

        public T Target { get; private set; }

        public Runner Runner { get; private set; }

        protected virtual void OnIntercepted(object sender, IInvocation e)
        {
            e.ReturnValue = this.Runner.Run(e.Method, e.GenericArguments, e.Arguments);
        }

        public void Dispose()
        {
            this.Runner.Dispose();
        }
    }
}
