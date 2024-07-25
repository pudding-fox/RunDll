using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace RunDll
{
    public class Handler
    {
        public Handler(IPEndPoint endpoint, Socket socket)
        {
            this.Endpoint = endpoint;
            this.Socket = socket;
        }

        public IPEndPoint Endpoint { get; private set; }

        public Socket Socket { get; private set; }

        public void Handle()
        {
            try
            {
                var request = Serializer.Deserialize<RunRequest>(this.Socket.ReceiveAll());
                var response = this.Handle(request);
                this.Socket.Send(Serializer.Serialize(response));
            }
            catch
            {
                //Nothing can be done.
            }
        }

        public RunResponse Handle(RunRequest request)
        {
            var assembly = Assembly.LoadFrom(request.Assembly);
            var type = assembly.GetType(request.Type);
            if (type == null)
            {
                type = Resolve(request.Type);
            }
            if (type.IsInterface)
            {
                type = Resolve(type);
            }
            var instance = GetTarget(ref type, Activator.CreateInstance(type), request.Config);
            var method = type.GetMethod(request.Method);
            var result = method.Invoke(instance, request.Arguments);
            return new RunResponse(result);
        }

        private static Type Resolve(string name)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Reverse())
            {
                var types = default(IEnumerable<Type>);
                try
                {
                    types = assembly.GetExportedTypes();
                }
                catch (ReflectionTypeLoadException e)
                {
                    types = e.Types;
                }
                catch (Exception e)
                {
                    continue;
                }
                foreach (var type in types)
                {
                    if (string.Equals(type.AssemblyQualifiedName, name, StringComparison.OrdinalIgnoreCase))
                    {
                        return type;
                    }
                }
                foreach (var type in types)
                {
                    if (string.Equals(type.FullName, name, StringComparison.OrdinalIgnoreCase))
                    {
                        return type;
                    }
                }
                foreach (var type in types)
                {
                    if (string.Equals(type.Name, name, StringComparison.OrdinalIgnoreCase))
                    {
                        return type;
                    }
                }
            }
            throw new NotImplementedException();
        }

        private static Type Resolve(Type @interface)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Reverse())
            {
                var types = default(IEnumerable<Type>);
                try
                {
                    types = assembly.GetExportedTypes();
                }
                catch (ReflectionTypeLoadException e)
                {
                    types = e.Types;
                }
                catch (Exception e)
                {
                    continue;
                }
                foreach (var type in types)
                {
                    if (type.GetInterfaces().Contains(@interface))
                    {
                        return type;
                    }
                }
            }
            throw new NotImplementedException();
        }

        private static object GetTarget(ref Type type, object instance, object config)
        {
            var method = type.GetMethod("GetTarget");
            if (method != null)
            {
                instance = method.Invoke(instance, new[] { config });
                type = instance.GetType();
            }
            return instance;
        }
    }
}
