using System.Reflection;
using System;

namespace RunDll
{
    public interface IInvocation
    {
        MethodInfo Method { get; }

        Type[] GenericArguments { get; }

        object[] Arguments { get; }

        object ReturnValue { get; set; }
    }
}
