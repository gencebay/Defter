using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Defter.SharedLibrary
{
    public static class HostingFactory
    {
        public const string DefaultCulture = "tr-TR";

        public static readonly List<CultureInfo> SupportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("tr-TR"),
            new CultureInfo("en"),
            new CultureInfo("tr")
        };
    }
}
