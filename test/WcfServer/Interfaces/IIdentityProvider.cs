using System.Security.Principal;

namespace WcfServer
{
    public interface IIdentityProvider
    {
        IPrincipal Principal { get; }
    }
}