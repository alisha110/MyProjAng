using AD_Manager.Layers.Model;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace AD_Manager.Layers.Authentication
{
    public class JwtService
    {
        //private readonly string _secretRse = "wEDFG23wEDFG23#@";
        private readonly SymmetricSecurityKey _tokenDecryptionKey;
        //private readonly string _secret;
        private readonly SymmetricSecurityKey _issuerSigningKey;
        //private readonly int _expDate;
        private static string _issuer;
        private static string _audience;
        public JwtService(AppSettings config)
        {
            //_secret = config.secret_Code_2;
            //_expDate = config.expirationInMinutes;
            _tokenDecryptionKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(config.SecretCode1));
            _issuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(config.SecretCode2));
            _issuer = config.Issuer;
            _audience = config.Audience;
        }

        public string GenerateSecurityToken(User userName)
        {
            try
            {
                //TokenHistories.RemoveToken(UserName);
                var tokenHandler = new JwtSecurityTokenHandler();
                //var key = Encoding.ASCII.GetBytes(_secret);

                var subject = new ClaimsIdentity();
                foreach (var item in userName.Permitions)
                {
                    subject.AddClaim(new Claim(Permision.Permisions.Permision, item));
                }

                subject.AddClaim(new Claim(ClaimTypes.Name, userName.UserName));

                var signingCredentials = new SigningCredentials(
                    _issuerSigningKey,
                    SecurityAlgorithms.HmacSha256);
                var ep = new EncryptingCredentials(_tokenDecryptionKey,
                    SecurityAlgorithms.Aes128KW,
                    SecurityAlgorithms.Aes128CbcHmacSha256);

                var token = tokenHandler.CreateJwtSecurityToken(
                    _issuer,
                    _audience,
                    new ClaimsIdentity(subject),
                    DateTime.Now,
                    DateTime.Now.AddHours(6),
                    DateTime.Now,
                    signingCredentials,
                    ep);
                return tokenHandler.WriteToken(token); ;
            }
            catch
            {
                // ignored
            }

            return null;
        }
    }

    public class TokenHistories
    {
        private static readonly List<TokenHistories> _Tokens = new List<TokenHistories>();
        public static void AddToken(string email, string ipAddress, string Token)
        {
            _Tokens.Add(new TokenHistories()
            {
                Email = email,
                ipAddress = ipAddress,
                token = string.Format("Bearer {0}", Token)
            });
        }
        public static bool TokenIsValied(string token, string ipAddress)
        {
            try
            {
                var t = _Tokens.FirstOrDefault(i => i.token == token && i.ipAddress == ipAddress);
                if (t != null)
                {

                    return true;
                }
            }
            catch { }
            return false;
        }

        public static bool TokenIsValied(Microsoft.AspNetCore.Http.HttpRequest req)
        {
            try
            {
                string remoteIpAddress = req.HttpContext.Connection.RemoteIpAddress.ToString();
                var accessToken = req.Headers[HeaderNames.Authorization].ToString();
                return TokenIsValied(accessToken, remoteIpAddress);
            }
            catch { }

            return false;
        }

        public static void CheckToken(Microsoft.AspNetCore.Http.HttpRequest req)
        {
            if (TokenIsValied(req)) return;
            throw new HttpStatusException(HttpStatusCode.BadRequest, "User not found");
        }

        public static void RemoveToken(string email)
        {
            var t = _Tokens.FirstOrDefault(i => i.Email == email);
            if (t != null)
                _Tokens.Remove(t);
        }
        public string token { get; set; }
        public string ipAddress { get; set; }
        public string Email { get; set; }
        public string expireDate { get; set; }
    }

    public class HttpStatusException : Exception
    {
        public HttpStatusCode Status { get; private set; }

        public HttpStatusException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }
    }
}

