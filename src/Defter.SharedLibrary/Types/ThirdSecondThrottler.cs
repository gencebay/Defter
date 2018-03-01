using System;
using System.Threading;

namespace Defter.SharedLibrary
{
    public class ThirdSecondThrottler : IThrottler
    {
        public ThirdSecondThrottler()
        {
        }

        public void Throttle(CancellationToken token)
        {
            while (DateTime.Now.Second % 3 != 0)
            {
                WaitASecondOrThrowIfCanceled(token);
            }
        }

        public void Delay(CancellationToken token)
        {
            WaitASecondOrThrowIfCanceled(token);
        }

        private static void WaitASecondOrThrowIfCanceled(CancellationToken token)
        {
            token.WaitHandle.WaitOne(TimeSpan.FromSeconds(1));
            token.ThrowIfCancellationRequested();
        }
    }
}
