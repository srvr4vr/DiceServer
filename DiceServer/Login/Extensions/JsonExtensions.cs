using Newtonsoft.Json;

namespace DiceServer.Login.Extensions
{
    public static class JsonExtensions
    {
        public static T FromJson<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default;
            }
        }

        public static string ToJson(this object @object) =>
            JsonConvert.SerializeObject(@object);
    }
}