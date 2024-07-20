using System;

namespace RunDll
{
    public interface IServer<T> : IDisposable
    {
        T Target { get; }
    }
}
