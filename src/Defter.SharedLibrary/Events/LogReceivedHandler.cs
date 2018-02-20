using Defter.SharedLibrary.Types;
using System;
using System.Threading.Tasks;

namespace Defter.SharedLibrary
{
    public class LogReceivedHandler : DomainHandlerBase<LogReceivedEvent>
    {
        private readonly RedisDomainEventStorage _eventStorage;
        public LogReceivedHandler(RedisDomainEventStorage eventStorage)
        {
            _eventStorage = eventStorage;
        }

        public override async Task HandleAsync(LogReceivedEvent evnt)
        {
            if (evnt == null)
            {
                throw new ArgumentNullException(nameof(evnt));
            }

            await _eventStorage.SaveEventAsync(evnt);
        }
    }
}
