using System;

namespace RunDll
{
    public interface IClient<T> : IDisposable
    {
        T Target { get; }
    }
}
