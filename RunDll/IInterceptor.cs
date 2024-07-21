using System;

namespace RunDll
{
    public interface IInterceptor
    {
        event EventHandler<global::RunDll.IInvocation> Intercepted;
    }
}
