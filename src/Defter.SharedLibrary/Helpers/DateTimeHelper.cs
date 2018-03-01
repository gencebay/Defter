using System;

namespace Defter.SharedLibrary.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddMilliseconds(javaTimeStamp).ToLocalTime();
            return dt;
        }
    }
}
