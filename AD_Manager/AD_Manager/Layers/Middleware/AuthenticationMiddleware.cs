using AD_Manager.Layers.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AD_Manager.Layers.Middleware
{
    public static class AuthenticationMiddleware
    {
        private static SymmetricSecurityKey _tokenDecryptionKey;
        private static SymmetricSecurityKey _issuerSigningKey;
        private static string _issuer;
        private static string _audience;
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services,
            AppSettings config)
        {
            _tokenDecryptionKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(config.SecretCode1));
            _issuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(config.SecretCode2));
            _issuer = config.Issuer;
            _audience = config.Audience;
            services.AddAuthorization(option =>
            {
                option.AddPolicy("GetAllUsers",
                    policy => policy.RequireClaim("GTUser", "True"));
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudiences = new[]
                    {
                        _audience
                    },
                    ValidIssuers = new[]
                    {
                        _issuer
                    },
                    IssuerSigningKey = _issuerSigningKey,
                    TokenDecryptionKey = _tokenDecryptionKey
                };
            });

            return services;
        }
    }
}
