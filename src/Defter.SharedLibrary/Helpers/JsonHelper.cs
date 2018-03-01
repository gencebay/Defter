using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Defter.SharedLibrary.Helpers
{
    public static class JsonHelper
    {
        public static string Serialize(object obj, bool camelCase = false)
        {
            var settings = new JsonSerializerSettings();
            if (camelCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            return JsonConvert.SerializeObject(obj, settings);
        }

        public static byte[] SerializeToBytes(object obj, bool camelCase = false)
        {
            return Encoding.UTF8.GetBytes(Serialize(obj, camelCase));
        }
    }
}