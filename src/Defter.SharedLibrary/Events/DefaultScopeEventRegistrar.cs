using System.Collections.Generic;
using System.Linq;

namespace Defter.SharedLibrary
{
    public class DefaultScopeEventRegistrar : IScopeEventRegistrar
    {
        IList<IDomainEventIdentity> ScopedEvents { get; }

        public DefaultScopeEventRegistrar()
        {
            ScopedEvents = new List<IDomainEventIdentity>();
        }

        public void AddToScope<TEvent>(TEvent evnt) where TEvent : IDomainEventIdentity
        {
            ScopedEvents.Add(evnt);
        }

        public IEnumerable<IDomainEventIdentity> GetScopedEvents()
        {
            return ScopedEvents.Distinct();
        }
    }
}