using System.IO;
using System.Xml.Serialization;

namespace RunDll
{
    public static class Serializer
    {
        public static byte[] Serialize(object value)
        {
            var serializer = new XmlSerializer(value.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, value);
                return stream.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] buffer)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new MemoryStream(buffer))
            {
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}
