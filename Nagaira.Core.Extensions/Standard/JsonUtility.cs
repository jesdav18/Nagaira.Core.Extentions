using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nagaira.Core.Extentions.Standard
{
    public static class JsonUtility
    {
        public static string Serialize(this int value)
        {
            return JsonConvert.SerializeObject(value);
        }
        public static string Serialize(this List<int> value)
        {
            return JsonConvert.SerializeObject(value);
        }
        public static string Serialize(this List<string> value)
        {
            return JsonConvert.SerializeObject(value);
        }
        public static string Serialize(this string value)
        {
            return JsonConvert.SerializeObject(value);
        }
        public static string Serialize<TEntity>(this List<TEntity> value) where TEntity : class
        {
            return JsonConvert.SerializeObject(value);
        }
        public static string Serialize<TEntity>(this TEntity value) where TEntity : class
        {
            return JsonConvert.SerializeObject(value);
        }
        public static List<TEntity>? Deserializes<TEntity>(this string value) where TEntity : class
        {
            return JsonConvert.DeserializeObject<List<TEntity>>(value);
        }
        public static TEntity? Deserialize<TEntity>(this string value) where TEntity : class
        {
            return JsonConvert.DeserializeObject<TEntity>(value);
        }
    }
}
