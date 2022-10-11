using AD_Manager.Layers.ADManagment;
using AD_Manager.Layers.DAL;
using AD_Manager.Layers.Model;
using System.Data;

namespace AD_Manager.Layers.BLL
{

    
    public interface IUserManager
    {
        List<AdUserRespDTo> GetNewUsers();
        List<ReportDTO> GetReports();
        List<AdUserRespDTo> GetOldUsers();
        AdUserRespDTo GetUser(string UserID);
        AdUserRespDTo GetUserchangeStatuse(string UserID);
        string GetProposalUserName(string UserID);
        public bool CreateUser(AdUserDTO userDTO, out string password);
        Boolean ActiveUser(string UserID, string UserName);
        Boolean DeactiveUser(string UserID, string UserName);
        Boolean LoginUser(string UserName, string Password);

        long RegisterLog(LogInfo_Base logInfo);
    }
}
