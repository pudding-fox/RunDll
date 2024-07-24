using System.Text;

namespace RunDll
{
    public static class Serializer
    {
        public static byte[] Serialize(object value)
        {
            return Encoding.UTF8.GetBytes(SimpleJson.SimpleJson.SerializeObject(value));
        }

        public static T Deserialize<T>(byte[] buffer)
        {
            return SimpleJson.SimpleJson.DeserializeObject<T>(Encoding.UTF8.GetString(buffer));
        }
    }
}
