using AD_Manager.Layers.Model;

namespace AD_Manager.Layers.Authentication.UserService
{
    public interface IUserService
    {
        public User Authentication(string UserName, string Password);
        public IEnumerable<User> GetAll();
        public User GetThisUser();
    }
}
