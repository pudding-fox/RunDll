#if NET6_0_OR_GREATER

using System;
using System.Reflection;

namespace RunDll
{
    public static class Proxy
    {
        public static Interceptor Create<T>(out T proxy)
        {
            proxy = DispatchProxy.Create<T, Interceptor>();
            return (Interceptor)(object)proxy;
        }

        public class Interceptor : DispatchProxy, global::RunDll.IInterceptor
        {
            protected override object Invoke(MethodInfo targetMethod, object[] args)
            {
                return this.Intercept(targetMethod, new Type[] { }, args);
            }

            public object Intercept(MethodInfo method, Type[] genericArguments, object[] arguments)
            {
                if (this.Intercepted == null)
                {
                    return null;
                }
                var invocation = new Invocation(method, genericArguments, arguments);
                this.Intercepted(this, invocation);
                return invocation.ReturnValue;
            }

            public event EventHandler<global::RunDll.IInvocation> Intercepted;
        }

        public class Invocation : global::RunDll.IInvocation
        {
            public Invocation(MethodInfo method, Type[] genericArguments, object[] arguments)
            {
                this.Method = method;
                this.GenericArguments = genericArguments;
                this.Arguments = arguments;
            }

            public MethodInfo Method { get; private set; }

            public Type[] GenericArguments { get; private set; }

            public object[] Arguments { get; private set; }

            public object ReturnValue { get; set; }
        }
    }
}

#endif