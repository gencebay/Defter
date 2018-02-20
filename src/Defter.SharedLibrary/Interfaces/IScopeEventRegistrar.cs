using System.Collections.Generic;

namespace Defter.SharedLibrary
{
    public interface IScopeEventRegistrar
    {
        IEnumerable<IDomainEventIdentity> GetScopedEvents();
        void AddToScope<TEvent>(TEvent evnt) where TEvent : IDomainEventIdentity;
    }
}