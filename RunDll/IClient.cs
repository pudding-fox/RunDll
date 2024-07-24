using System;

namespace RunDll
{
    public interface IClient<T> 
    {
        T Target { get; }
    }
}
