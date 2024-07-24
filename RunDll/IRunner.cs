using System;

namespace RunDll
{
    public interface IRunner : IDisposable
    {
        object Run(string assembly, string type, string method, object config, object[] arguments);
    }
}
