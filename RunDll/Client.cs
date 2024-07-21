namespace RunDll
{
    public class Client<T> : IClient<T>
    {
        public Client(Runtime runtime, Mapping mapping)
        {
            var target = default(T);
            this.Interceptor = Proxy.Create<T>(out target);
            this.Interceptor.Intercepted += this.OnIntercepted;
            this.Target = target;
            this.Runner = new Runner(runtime);
            this.Mapping = mapping;
        }

        public Proxy.Interceptor Interceptor { get; private set; }

        public T Target { get; private set; }

        public Runner Runner { get; private set; }

        public Mapping Mapping { get; private set; }

        protected virtual void OnIntercepted(object sender, IInvocation e)
        {
            var assembly = default(string);
            var type = default(string);
            this.Mapping.Resolve(e.Method, out assembly, out type);
            e.ReturnValue = this.Runner.Run(assembly, type, e.Method.Name, e.Arguments);
        }

        public void Dispose()
        {
            this.Runner.Dispose();
        }
    }
}
