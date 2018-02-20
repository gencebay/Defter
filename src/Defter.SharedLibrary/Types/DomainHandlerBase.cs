using NetCoreStack.Contracts;
using System.Threading.Tasks;

namespace Defter.SharedLibrary.Types
{
    public abstract class DomainHandlerBase<T> : IDomainHandler, IDomainHandler<T> where T : IDomainEvent
    {
        public abstract Task HandleAsync(T evnt);

        public virtual Task GetHandler(object evnt)
        {
            if (typeof(T).IsAssignableFrom(evnt.GetType()))
            {
                return HandleAsync((T)evnt);
            }

            return Task.CompletedTask;
        }
    }
}