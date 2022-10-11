using AD_Manager.Layers.Authentication.UserService;
using AD_Manager.Layers.BLL;
using AD_Manager.Layers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AD_Manager.Controllers
{
    //[Authorize]
    [Route("/")]
    [ApiController]
    public class LoginController : Controller
    {
        //private readonly ILogger _logger;
        public readonly IUserService _userManager;
        public LoginController(IUserService userManager/*, ILogger logger*/)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(User user)
        {
            try
            {
                var authUser = _userManager.Authentication(user.UserName, user.password);
                if (authUser == null)
                    return BadRequest(new Errors() { ErrorMessages = "نام کاربری یا رمز عبور صحیح نمی باشد." });
                return Ok(authUser._token); 
                    //Ok(_userManager.LoginUser(user.UserName, user.password));
            }
            catch //(Exception ex)
            {
                Errors errors = new(){
                    ErrorMessages = "نام کاربری یا رمز عبور صحیح نمی باشد."//ex.Message
                };
                return BadRequest(errors);
            }
        }
    }
}
