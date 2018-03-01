using System.Threading;

namespace Defter.SharedLibrary
{
    public interface IThrottler
    {
        void Throttle(CancellationToken token);
        void Delay(CancellationToken token);
    }
}
