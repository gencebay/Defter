using System;
using System.Collections.Generic;

namespace Defter.Api.Hosting
{
    public interface IScheduleInstant
    {
        DateTime NowInstant { get; }
        DateTime? NextInstant { get; }
        IEnumerable<DateTime> GetNextInstants(DateTime lastInstant);
    }
}
