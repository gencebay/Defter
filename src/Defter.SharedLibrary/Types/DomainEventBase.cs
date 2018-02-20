namespace Defter.SharedLibrary
{
    public class DomainEventBase : IDomainEventIdentity
    {
        public DomainEventBase()
        {

        }

        public virtual string Id { get; }
    }
}
