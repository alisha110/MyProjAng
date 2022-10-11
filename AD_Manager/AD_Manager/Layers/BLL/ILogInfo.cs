namespace AD_Manager.Layers.BLL
{
    public interface ILogInfo
    {
        Task<long> Error(string UserName, string Message);
        Task<long> Info(string UserName, string Message);
        Task<long> Werrning(string UserName, string Message);
        void UpdateLog(long Id, bool Result, string ErrorMessage);
    }

   
}
