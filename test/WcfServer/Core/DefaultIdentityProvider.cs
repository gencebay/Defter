using System.Security.Claims;
using System.Security.Principal;
using System.ServiceModel;

namespace WcfServer
{
    public class DefaultIdentityProvider : IIdentityProvider
    {
        public IPrincipal Principal
        {
            get
            {
                if (OperationContext.Current.ServiceSecurityContext == null)
                {
                    return new ClaimsPrincipal();
                }

                return new GenericPrincipal(OperationContext.Current.ServiceSecurityContext?.PrimaryIdentity, new string[] { "NetCoreStack" });
            }
        }
    }
}
