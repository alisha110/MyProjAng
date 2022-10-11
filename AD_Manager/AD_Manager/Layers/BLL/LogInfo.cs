using System.Security.Claims;

namespace AD_Manager.Layers.BLL
{
    public class LogInfo : ILogInfo
    {
        public readonly IUserManager _userManager;
        public LogInfo(IUserManager userManager)
        {
            _userManager = userManager;
        }

        private async Task<long> RegisterLog(string UserName, string Message, LogInfoType LogLevel)
        {
            //var t = ControlBase.User.FindFirst(ClaimTypes.Name);

            LogInfo_Base logInfo = new ()
            {
               Message = Message,
               UserName = UserName,
               Level = LogLevel
            };
            return _userManager.RegisterLog(logInfo);
            //return 0;
        }
        public async Task<long> Error(string UserName, string Message)
        {
            return await RegisterLog(UserName, Message, LogInfoType.Error);
            //throw new NotImplementedException();
        }

        public async Task<long> Info(string UserName, string Message)
        {
            return await RegisterLog(UserName, Message, LogInfoType.Information);
            //throw new NotImplementedException();
        }

        public void UpdateLog(long Id, bool Result, string ErrorMessage)
        {
            //return RegisterLog(UserName, Message, LogInfoType.Error);
            throw new NotImplementedException();
        }

        public async Task<long> Werrning(string UserName, string Message)
        {
            return await RegisterLog(UserName, Message, LogInfoType.Warning  );
            //throw new NotImplementedException();
        }
    }
}
