using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AD_Manager.Layers.Authentication.Permision
{
    public class PermisionProvider : DefaultAuthorizationPolicyProvider
    {
        public PermisionProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (!policyName.StartsWith(PermisionAuth.PerfixPer))
                return base.GetPolicyAsync(policyName);

            var permNames = policyName.Replace("Permision: ", "").Split(",");
            var policy = new AuthorizationPolicyBuilder()
                .RequireClaim(Permisions.Permision, permNames)
                .Build();

            return Task.FromResult(policy);
        }
    }
}
