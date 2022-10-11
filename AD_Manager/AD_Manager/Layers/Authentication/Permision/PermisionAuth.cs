using Microsoft.AspNetCore.Authorization;

namespace AD_Manager.Layers.Authentication.Permision
{
    public class PermisionAuth : AuthorizeAttribute
    {
        internal const string PerfixPer = "Permision: ";
        public PermisionAuth(params string[] permisions)
        {
            Policy = $"{PerfixPer}{string.Join(",", permisions)}";
        }
    }
}
