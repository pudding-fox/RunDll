using System.Collections.Generic;
using System.Reflection;

namespace RunDll
{
    public interface IMapping : IDictionary<Mapping.Key, Mapping.Value>
    {
        void Resolve(MethodInfo method, out string assembly, out string type, out object config);
    }
}
