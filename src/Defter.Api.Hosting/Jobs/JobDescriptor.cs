using NCrontab;
using System;

namespace Defter.Api.Hosting
{
    public class JobDescriptor : IEquatable<JobDescriptor>
    {
        public string Id { get; }
        public Type Type { get; }
        public Type ThrottlerType { get; set; }
        public CrontabSchedule Cron { get; set; }

        public JobDescriptor(Type type)
        {
            Id = Guid.NewGuid().ToString("N");
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public bool Equals(JobDescriptor other)
        {
            return other != null && other.Id.Equals(Id, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as JobDescriptor);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(JobDescriptor left, JobDescriptor right)
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

        public static bool operator !=(JobDescriptor left, JobDescriptor right)
        {
            return !(left == right);
        }
    }
}
