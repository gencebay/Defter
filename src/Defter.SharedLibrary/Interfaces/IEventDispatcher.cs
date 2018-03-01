using System.Threading.Tasks;

namespace Defter.SharedLibrary
{
    public interface IEventDispatcher
    {
        Task Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent;
    }
}
