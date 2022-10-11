namespace AD_Manager.Layers.BLL
{
    public class LogInfo_Base
    {
        public long ID { get; set; }
        public LogInfoType Level { get; set; }
        public string Message { get; set; }
        public bool Result { get; set; }
        public string ErrorMessage { get; set; }
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }

    }

    public enum LogInfoType
    {
        Error = 0,
        Warning = 1,
        Information = 2,
        WarningInformation = 3,
        InformationInformation = 4
    }
}
