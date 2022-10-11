using System.Data;

namespace AD_Manager.Layers.Model
{
    public class ReportDTO
    {
        public long ID { get; set; }
        public int? Level { get; set; }
        public string? Message { get; set; }
        public bool Result { get; set; }
        public string? ErrorMessage { get; set; }
        public string? UserName { get; set; }
        public DateTime? RegisterDate { get; set; }

        public static List<ReportDTO> ToAdUserDTOList(DataTable dt)
        {
            List<ReportDTO> _List = new List<ReportDTO>();
            _List = (from DataRow dr in dt.Rows
                     select new ReportDTO()
                     {
                         ID = dr["ID"].ToLong(),
                         Level = dr["Level"].ToInt(),
                         Message = dr["Message"].ToString(),
                         ErrorMessage = dr["ErrorMessage"].ToString(),
                         Result = dr["Result"].ToBool(),
                         UserName = dr["UserName"].ToString(),
                         RegisterDate = dr["RegisterDate"].ToDateTime(),
                     }).ToList();
            return _List;
        }
    }
}
