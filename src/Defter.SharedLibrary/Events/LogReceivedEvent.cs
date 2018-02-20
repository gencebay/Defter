using System;

namespace Defter.SharedLibrary
{
    public class LogReceivedEvent : DomainEventBase, IEquatable<LogReceivedEvent>
    {
        public string AuditRequestId { get; set; }
        public DateTime ReceivedUtcTime { get; set; }
        public DefterGenericMessage Message { get; set; }
        public override string Id => AuditRequestId;

        public bool Equals(LogReceivedEvent other)
        {
            return other != null && other.Id.Equals(Id, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LogReceivedEvent);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(LogReceivedEvent left, LogReceivedEvent right)
        {
            if ((object)left == null)
            {
                return ((object)right == null);
            }
            else if ((object)right == null)
            {
                return ((object)left == null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(LogReceivedEvent left, LogReceivedEvent right)
        {
            return !(left == right);
        }
    }
}
