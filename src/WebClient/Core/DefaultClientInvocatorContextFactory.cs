using Microsoft.Extensions.Options;
using NetCoreStack.WebSockets.ProxyClient;
using System;
using static WebClient.Core.WebClientConstants;

namespace WebClient.Core
{
    public class DefaultClientInvocatorContextFactory : IClientInvocatorContextFactory<ProxyWebSocketDataStream>
    {
        private readonly ApiSettings _options;

        public DefaultClientInvocatorContextFactory(IOptions<ApiSettings> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            if (optionsAccessor.Value == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.Value;
        }

        public ClientInvocatorContext CreateInvocatorContext()
        {
            return new ClientInvocatorContext(typeof(ProxyWebSocketDataStream), ConnectorName, _options.Host);
        }
    }
}
