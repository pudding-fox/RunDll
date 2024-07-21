using System;
using System.Collections.Generic;
using System.Reflection;

namespace RunDll
{
    public class Mapping : Dictionary<Mapping.Key, Mapping.Value>
    {
        public Mapping()
        {

        }

        public void Resolve(MethodInfo method, out string assembly, out string type)
        {
            var key = new Key(method.DeclaringType.Assembly.Location, method.DeclaringType.AssemblyQualifiedName);
            var value = default(Value);
            this.TryGetValue(key, out value);
            assembly = value.Assembly;
            type = value.Type;
        }

        public class Key : IEquatable<Key>
        {
            public Key(Type type) : this(type.Assembly.Location, type.AssemblyQualifiedName)
            {

            }

            public Key(string assembly, string type)
            {
                this.Assembly = assembly;
                this.Type = type;
            }

            public string Assembly { get; private set; }

            public string Type { get; private set; }

            public override bool Equals(object obj)
            {
                return this.Equals(obj as Key);
            }

            public bool Equals(Key other)
            {
                if (other == null)
                {
                    return false;
                }
                if (object.ReferenceEquals(this, other))
                {
                    return true;
                }
                if (!string.Equals(this.Assembly, other.Assembly, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                if (!string.Equals(this.Type, other.Type, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                return true;
            }

            public override int GetHashCode()
            {
                var hashCode = default(int);
                unchecked
                {
                    if (!string.IsNullOrEmpty(this.Assembly))
                    {
                        hashCode += this.Assembly.GetHashCode();
                    }
                    if (!string.IsNullOrEmpty(this.Type))
                    {
                        hashCode += this.Type.GetHashCode();
                    }
                }
                return hashCode;
            }
        }

        public class Value
        {
            public Value(Type type) : this(type.Assembly.Location, type.AssemblyQualifiedName)
            {

            }

            public Value(string assembly, string type)
            {
                this.Assembly = assembly;
                this.Type = type;
            }

            public string Assembly { get; private set; }

            public string Type { get; private set; }
        }
    }
}
