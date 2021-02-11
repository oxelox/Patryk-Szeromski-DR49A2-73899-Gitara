using System.Security.Principal;

namespace Gitara.Helpers
{
    public static class IdentityHelper
    {
        public static int ID(this IIdentity identity)
        {
            if (identity == null)
                return 0;

            return int.Parse(identity.Name);
        }
    }
}