using Microsoft.Extensions.Options;

namespace Defter.Api.Hosting
{
    public class RedisStorageOptions : IOptions<RedisStorageOptions>
    {
        public string Configuration { get; set; }

        public string InstanceName { get; set; }

        public RedisStorageOptions Value
        {
            get { return this; }
        }
    }
}
