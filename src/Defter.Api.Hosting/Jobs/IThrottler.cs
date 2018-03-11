using System.Threading;

namespace Defter.Api.Hosting
{
    public interface IThrottler
    {
        void Throttle(CancellationToken token);
        void Delay(CancellationToken token);
    }
}
