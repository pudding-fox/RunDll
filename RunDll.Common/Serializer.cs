using Newtonsoft.Json;
using System.Text;

namespace RunDll
{
    public static class Serializer
    {
        public static byte[] Serialize(object value)
        {
            var text = JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return Encoding.UTF8.GetBytes(text);
        }

        public static T Deserialize<T>(byte[] buffer)
        {
            var text = Encoding.UTF8.GetString(buffer);
            return JsonConvert.DeserializeObject<T>(text, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }
    }
}
