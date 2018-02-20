using NetCoreStack.Contracts;
using System.Threading.Tasks;

namespace Defter.SharedLibrary.Types
{
    public class TaskEventHandlerDefinition
    {
        public IDomainEvent Event { get; }
        public Task Task { get; }

        public TaskEventHandlerDefinition(Task task, IDomainEvent evnt)
        {
            Task = task;
            Event = evnt;
        }
    }
}
