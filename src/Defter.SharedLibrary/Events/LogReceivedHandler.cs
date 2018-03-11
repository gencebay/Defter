using Defter.SharedLibrary.Types;
using System;
using System.Threading.Tasks;

namespace Defter.SharedLibrary
{
    public class LogReceivedHandler : DomainHandlerBase<LogReceivedEvent>
    {
        private readonly IEventDispatcher _eventDispatcher;

        public LogReceivedHandler(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public override async Task HandleAsync(LogReceivedEvent evnt)
        {
            if (evnt == null)
            {
                throw new ArgumentNullException(nameof(evnt));
            }

            await _eventDispatcher.Dispatch(evnt);
        }
    }
}
