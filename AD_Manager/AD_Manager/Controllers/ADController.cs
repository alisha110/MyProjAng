using AD_Manager.Layers;
using AD_Manager.Layers.ADManagment;
using AD_Manager.Layers.Authentication.Permision;
using AD_Manager.Layers.BLL;
using AD_Manager.Layers.DAL;
using AD_Manager.Layers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//using System.Web.Http;

namespace AD_Manager.Controllers
{
    [Authorize]
    [PermisionAuth(Permisions.GetAll)]
    [Route("api/[controller]")]
    [ApiController]
    public class ADController : Controller
    {
        private readonly ILogger _logger;
        public readonly IUserManager _userManager;
        public ILogInfo _LogInfo { get; set; }
        public ADController(ILogger<Controller> logger, ILogInfo LogInfo, IUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
            _LogInfo = LogInfo;
        }

        [HttpPost]
        [Route("GetUserList")]
        [ProducesResponseType(200, Type = typeof(List<AdUserRespDTo>))]
        [ProducesResponseType(400, Type = typeof(Errors))]
        public IActionResult GetUserList()
        {
            try
            {
                return Ok(_userManager.GetNewUsers());
            }
            catch (Exception ex)
            {
                Errors errors = new Errors();
                errors.ErrorMessages = ex.Message;

                return BadRequest(errors);
            }
        }

        [HttpPost]
        [Route("GetOldUserList")]
        [ProducesResponseType(200, Type = typeof(List<AdUserRespDTo>))]
        [ProducesResponseType(400, Type = typeof(Errors))]
        public IActionResult GetOldUserList()
        {
            try
            {
                return Ok(_userManager.GetOldUsers());
            }
            catch (Exception ex)
            {
                Errors errors = new Errors();
                errors.ErrorMessages = ex.Message;

                return BadRequest(errors);
            }
        }

        [HttpPost]
        [Route("GetUser")]
        [ProducesResponseType(200, Type = typeof(AdUserRespDTo))]
        [ProducesResponseType(400, Type = typeof(Errors))]
        public IActionResult GetUser(string UserID)
        {
            try
            {
                return Ok(_userManager.GetUser(UserID));
            }
            catch (Exception ex)
            {
                Errors errors = new Errors();
                errors.ErrorMessages = ex.Message;

                return BadRequest(errors);
            }
        }

        [HttpPost]
        [Route("GetProposalUserName")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(Errors))]
        public IActionResult GetProposalUserName(string UserID)
        {
            try
            {
                return Ok(_userManager.GetProposalUserName(UserID));
            }
            catch (Exception ex)
            {
                Errors errors = new Errors();
                errors.ErrorMessages = ex.Message;

                return BadRequest(errors);
            }
        }

        [HttpPost]
        [Route("CreateUserInAD")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(Errors))]
        public IActionResult CreateUserInAD(AdUserDTO userDTO)
        {
            try
            {
                bool bl = _userManager.CreateUser(userDTO, out string password);
                if (bl)
                {
                    var t = User.FindFirst(ClaimTypes.Name);
                    _LogInfo.Info(t.Value, string.Format("Create User {0}", userDTO.UserName));
                }
                return Ok(password);
            }
            catch (Exception ex)
            {
                Errors errors = new Errors();
                errors.ErrorMessages = ex.Message;

                return BadRequest(errors);
            }
            //List<AdUserRespDTo> adUserList = _userManager.GetNewUsers();
            //return adUserList;
        }

        [HttpPost]
        [Route("ActiveDeactiveUserInAD")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400, Type = typeof(Errors))]
        public IActionResult ActiveDeactiveUserInAD(string UserID)
        {
            try
            {
                //var userID = "";
                var Userd = _userManager.GetUserchangeStatuse(UserID);
                if (Userd == null || !Userd.PersonCode.IsnotEmpty())
                    throw new Exception("کاربر مورد نظر را یافت نشد");
                bool bl;
                if (Userd.shaghel)
                    bl = _userManager.DeactiveUser(Userd.PersonCode, Userd.UserName);
                else
                    bl = _userManager.ActiveUser(Userd.PersonCode, Userd.UserName);
                if (bl)
                {
                    var t = User.FindFirst(ClaimTypes.Name);

                    _LogInfo.Info(t.Value, string.Format("Active User {0}", Userd.UserName));
                }
                return Ok(bl);
            }
            catch (Exception ex)
            {
                Errors errors = new Errors();
                errors.ErrorMessages = ex.Message;

                return BadRequest(errors);
            }
        }

        //[HttpPost]
        //[Route("DeactiveUser")]
        //[ProducesResponseType(200, Type = typeof(bool))]
        //[ProducesResponseType(400, Type = typeof(Errors))]
        //public IActionResult DeactiveUser(AdUserDTO userDTO)
        //{
        //    try
        //    {
        //        var bl = _userManager.DeactiveUser(userDTO.PersonCode);
        //        if (bl)
        //        {
        //            var t = User.FindFirst(ClaimTypes.Name);

        //            _LogInfo.Info(t.Value, string.Format("Deactive User {0}", userDTO.UserName));
        //        }
        //        return Ok(bl);
        //    }
        //    catch (Exception ex)
        //    {
        //        Errors errors = new Errors();
        //        errors.ErrorMessages = ex.Message;

        //        return BadRequest(errors);
        //    }
        //}

        [HttpPost]
        [Route("GetReport")]
        [ProducesResponseType(200, Type = typeof(List<ReportDTO>))]
        [ProducesResponseType(400, Type = typeof(Errors))]
        public IActionResult GetReport()
        {
            try
            {
                //return Ok(true);
                return Ok(_userManager.GetReports());
            }
            catch (Exception ex)
            {
                Errors errors = new Errors();
                errors.ErrorMessages = ex.Message;
                return BadRequest(errors);
            }
        }
    }
}