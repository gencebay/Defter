using NetCoreStack.Contracts;

namespace Defter.SharedLibrary
{
    public interface IDomainEventIdentity : IDomainEvent
    {
        string Id { get; }
    }
}
