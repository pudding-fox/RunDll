using System;
using System.Linq;
using System.Reflection;

namespace RunDll
{
    [Serializable]
    public class RunRequest
    {
        public RunRequest()
        {

        }

        public RunRequest(string location, string type, string method, string[] genericArguments, object[] arguments)
        {
            this.Location = location;
            this.Type = type;
            this.Method = method;
            this.GenericArguments = genericArguments;
            this.Arguments = arguments;
        }

        public RunRequest(MethodInfo method, Type[] genericArguments, object[] arguments)
        {
            this.Location = method.DeclaringType.Assembly.Location;
            this.Type = method.DeclaringType.AssemblyQualifiedName;
            this.Method = method.Name;
            if (genericArguments != null)
            {
                this.GenericArguments = genericArguments.Select(
                    type => type.AssemblyQualifiedName
                ).ToArray();
            }
            else
            {
                this.GenericArguments = new string[] { };
            }
            this.Arguments = arguments;
        }

        public string Location { get; set; }

        public string Type { get; set; }

        public string Method { get; set; }

        public string[] GenericArguments { get; set; }

        public object[] Arguments { get; set; }
    }
}
