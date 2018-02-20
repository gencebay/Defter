using Defter.SharedLibrary.Types;
using Microsoft.Extensions.DependencyInjection;
using NetCoreStack.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Defter.SharedLibrary
{
    public class DomainEventDispatcher : IEventDispatcher
    {
        protected IServiceProvider ApplicationServices { get; }

        public DomainEventDispatcher(IServiceProvider applicationServices)
        {
            ApplicationServices = applicationServices;
        }

        public Task Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent
        {
            var handler = ApplicationServices.GetService<IDomainHandler<TEvent>>();
            if (handler != null)
            {
                return handler.HandleAsync(eventToDispatch);
            }

            return Task.CompletedTask;
        }

        public IEnumerable<TaskEventHandlerDefinition> GetHandlers(params IDomainEvent[] events)
        {
            List<TaskEventHandlerDefinition> tasks = new List<TaskEventHandlerDefinition>();

            foreach (var evnt in events)
            {
                var handlerType = typeof(IDomainHandler<>).MakeGenericType(new Type[] { evnt.GetType() });
                var handler = ApplicationServices.GetService(handlerType);
                if (handler is IDomainHandler eventHandler)
                {
                    tasks.Add(new TaskEventHandlerDefinition(eventHandler.GetHandler(evnt), evnt));
                }
            }

            return tasks;
        }
    }
}
