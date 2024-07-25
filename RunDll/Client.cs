using System.IO;

namespace RunDll
{
    public class Client<T> : IClient<T>
    {
        public Client(IRunner runner) : this(runner, null)
        {

        }

        public Client(IRunner runner, IMapping mapping)
        {
            var target = default(T);
            this.Interceptor = Proxy.Create<T>(out target);
            this.Interceptor.Intercepted += this.OnIntercepted;
            this.Target = target;
            this.Runner = runner;
            this.Mapping = mapping;
        }

        public Proxy.Interceptor Interceptor { get; private set; }

        public T Target { get; private set; }

        public IRunner Runner { get; private set; }

        public IMapping Mapping { get; private set; }

        protected virtual void OnIntercepted(object sender, IInvocation e)
        {
            var assembly = default(string);
            var type = default(string);
            var config = default(object);
            if (this.Mapping != null)
            {
                this.Mapping.Resolve(e.Method, out assembly, out type, out config);
            }
            else
            {
                assembly = Path.GetFileName(e.Method.DeclaringType.Assembly.Location);
                type = e.Method.DeclaringType.FullName;
            }
            e.ReturnValue = this.Runner.Run(assembly, type, e.Method.Name, config, e.Arguments);
        }
    }
}
