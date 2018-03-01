using System.Threading.Tasks;

namespace Defter.SharedLibrary
{
    public interface IDomainHandler
    {
        Task GetHandler(object evnt);
    }

    public interface IDomainHandler<TEvent> where TEvent : IDomainEvent
    {
        Task HandleAsync(TEvent evnt);
    }
}