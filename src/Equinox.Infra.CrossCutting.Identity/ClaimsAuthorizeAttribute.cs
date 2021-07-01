using NetDevPack.Identity.Authorization;

namespace Equinox.Infra.CrossCutting.Identity
{
    public class ClaimsAuthorizeAttribute : CustomAuthorizeAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue)
            : base(claimName, claimValue)
        {
        }
    }
}
