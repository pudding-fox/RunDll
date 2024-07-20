#if NET48

using Castle.DynamicProxy;
using System;
using System.Reflection;

namespace RunDll
{
    public static class Proxy
    {
        public static Interceptor Create<T>(out T proxy)
        {
            var generator = new ProxyGenerator();
            var interceptor = new Interceptor();
            proxy = (T)generator.CreateInterfaceProxyWithoutTarget(typeof(T), interceptor);
            return interceptor;
        }

        public class Interceptor : global::Castle.DynamicProxy.IInterceptor, global::RunDll.IInterceptor
        {
            public void Intercept(global::Castle.DynamicProxy.IInvocation invocation)
            {
                invocation.ReturnValue = this.Intercept(invocation.Method, invocation.GenericArguments, invocation.Arguments);
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