using Newtonsoft.Json;

namespace Easy.Commerce.CrossCutting
{
    public static class StringExtensions
    {
        public static TClass JsonToObject<TClass>(this string value)
            where TClass : class
        {
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<TClass>(value);
            }

            return null;
        }
    }
}
