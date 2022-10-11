using AD_Manager.Layers.Model;
using Microsoft.Extensions.Options;

namespace AD_Manager.Layers.Authentication.UserService
{
    public class UserService:IUserService
    {
        private readonly AppSettings _config;
        public UserService(IOptions<AppSettings> config)
        {
            _config = config.Value;
        }

        readonly List<User> _users = new List<User>()
        {
            new User(){UserName = "hamid", password = "123", Role = "Admin", GTUser=true,
                Permitions = new[]{ Permision.Permisions.GetAll } },
            new User(){UserName = "ali", password = "123", Role = "User",
                Permitions = new[]{ Permision.Permisions.GetUsers }},
        };
        public User Authentication(string Username, string Password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return null;
            var us = _users.Where(c => c.UserName.ToLower() == Username.ToLower()
                && c.password == Password).FirstOrDefault();
            if (us != null)
            {
                var jwt = new JwtService(_config);
                var token = jwt.GenerateSecurityToken(us);
                if (token is null)
                    return null;
                User user = new User()
                {
                    UserName = us.UserName,
                    _token = token,
                    password = "",
                    Permitions = us.Permitions
                };
                return user;
            }
            return null;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.ToList();
        }
        public User GetThisUser()
        {
            return _users.Where(c => c.UserName == "").FirstOrDefault();
        }
    }
}
