using AD_Manager.Layers.ADManagment;
using AD_Manager.Layers.DAL;
using AD_Manager.Layers.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AD_Manager.Layers.BLL
{
    public class UserManager : IUserManager
    {
        public readonly IADHelper _adHelper;
        public readonly IDatabaseHelper _databaseHelper;

        public UserManager(IADHelper adHelper, IDatabaseHelper databaseHelper)
        {
            _adHelper = adHelper;
            _databaseHelper = databaseHelper;
        }

        public bool ActiveUser(string UserID, string UserName)
        {
            string message;
            //var UserDto = GetUser(UserID);
            var b = _adHelper.ActivateUser(UserName, out message);
            if (b)
            {
                string cmd = "UPDATE [Accounting].[Staff_AdStatuse] SET shaghel = @Shaghel " +
                    "WHERE PersonCode = @personCode ";
                SqlParameter[] sp = new SqlParameter[] {
                    new SqlParameter("@PersonCode", UserID),
                    new SqlParameter("@Shaghel", true),
                };
                _databaseHelper.ExequteNonQuery(cmd, out _, sp);
            }
            else
                throw new Exception(message);
            return b;
        }
        public bool DeactiveUser(string UserID, string UserName)
        {
            string message;
            //var UserDto = GetUser(UserID);
            var b = _adHelper.DeactivateUser(UserName, out message);

            if (b)
            {
                string cmd = "UPDATE [Accounting].[Staff_AdStatuse] SET shaghel = @Shaghel " +
                    "WHERE PersonCode = @personCode ";
                SqlParameter[] sp = new SqlParameter[] {
                    new SqlParameter("@PersonCode", UserID),
                    new SqlParameter("@Shaghel", false),
                };
                _databaseHelper.ExequteNonQuery(cmd, out _, sp);
            }
            else
                throw new Exception(message);
            return b;
        }
        public bool CreateUser(AdUserDTO userDTO, out string password)
        {
            var uDTO = GetUser(userDTO.PersonCode);
            uDTO.UserName = userDTO.UserName;
            var t = _adHelper.CreateUser(uDTO, out password);
            if (t == null)
            {
                throw new Exception(password);
            }
            string cmd = "INSERT INTO [Accounting].[Staff_AdStatuse] ([PersonCode], [Name], [Family], [NationalNo], [Email], [DocumentName], [JobStringName], [khedmat_unit], [unit_khedmat], [shaghel], [halat_eshteghal], [PersonNo], [Raste], [UniversityLevelName1]) " +
                "SELECT PersonCode, Name, Family, NationalNo, @Email, DocumentName, JobStringName, khedmat_unit, unit_khedmat, shaghel, halat_eshteghal, PersonNo, Raste, UniversityLevelName1 FROM Accounting.Staff t1 " +
                "WHERE t1.PersonCode = @PersonCode";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@PersonCode", userDTO.PersonCode),
                new SqlParameter("@Email", userDTO.UserName+ "@mums.ac.ir"),
            };
            _databaseHelper.ExecuteQuery(cmd, out _, sp);

            return true;
        }
        public List<AdUserRespDTo> GetNewUsers()
        {
            string message;
            //string cmd = "SELECT top 100 Name FirstName, Family LastName, NationalNo NationalCode, " +
            //    "DocumentName Grade, JobStringName, khedmat_unit Workplace, unit_khedmat, shaghel, " +
            //    "halat_eshteghal, PersonNo, PersonCode, Email FROM [Accounting].[Vw_Staff] order by NEWID()";
            string cmd = "SELECT Name FirstName, Family LastName, NationalNo NationalCode, " +
                "DocumentName Grade, JobStringName, khedmat_unit Workplace, unit_khedmat, shaghel, " +
                "halat_eshteghal, PersonNo, PersonCode, Email, '' UserName, '' ouplace FROM[Accounting].[Vw_Staff] " +
                "WHERE shaghel = 1 AND ISNULL(Email, '') NOT LIKE '%@mums.ac.ir'";
            using var dt = _databaseHelper.ExecuteQuery(cmd, out message);
            if (dt == null)
            {
                //List<AdUserRespDTo> l = new List<AdUserRespDTo>();
                //l.Add(new AdUserRespDTo()
                //{
                //    UserName ="Rabbanih3",
                //    //ID=1001,
                //    FirstName="حمید",
                //    LastName="ربانی",
                //    Grade="لیسانس",
                //    Major="کامپیوتر",
                //    NationalCode="0703938673",
                //    Password="123456",
                //    Workplace="ای تی"
                //});
                //l.Add(new AdUserRespDTo()
                //{
                //    UserName = "Rezaeim7",
                //    //ID = 1002,
                //    FirstName = "محمد",
                //    LastName = "رضایی",
                //    Grade = "لیسانس",
                //    Major = "کامپیوتر",
                //    NationalCode = "0946663254",
                //    Password = "123456",
                //    Workplace = "ای تی"
                //});
                //return l;
                throw new Exception(message);
            }
            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

            //return json;
            return AdUserRespDTo.ToAdUserDTOList(dt);
        }
        public List<AdUserRespDTo> GetOldUsers()
        {
            string message;
            //string cmd = "SELECT top 100 Name FirstName, Family LastName, NationalNo NationalCode, " +
            //    "DocumentName Grade, JobStringName, khedmat_unit Workplace, unit_khedmat, shaghel, " +
            //    "halat_eshteghal, PersonNo, PersonCode, Email FROM [Accounting].[Vw_Staff] order by NEWID()";
            string cmd = "SELECT Name FirstName, Family LastName, NationalNo NationalCode, " +
                "DocumentName Grade, JobStringName, khedmat_unit Workplace, unit_khedmat, shaghel, " +
                "halat_eshteghal, PersonNo, PersonCode, Email, '' UserName, '' ouplace FROM[Accounting].[VWStaff_ChangeStstuse] ";
            using var dt = _databaseHelper.ExecuteQuery(cmd, out message);
            if (dt == null)
            {
                //List<AdUserRespDTo> l = new List<AdUserRespDTo>();
                //l.Add(new AdUserRespDTo()
                //{
                //    UserName ="Rabbanih3",
                //    //ID=1001,
                //    FirstName="حمید",
                //    LastName="ربانی",
                //    Grade="لیسانس",
                //    Major="کامپیوتر",
                //    NationalCode="0703938673",
                //    Password="123456",
                //    Workplace="ای تی"
                //});
                //l.Add(new AdUserRespDTo()
                //{
                //    UserName = "Rezaeim7",
                //    //ID = 1002,
                //    FirstName = "محمد",
                //    LastName = "رضایی",
                //    Grade = "لیسانس",
                //    Major = "کامپیوتر",
                //    NationalCode = "0946663254",
                //    Password = "123456",
                //    Workplace = "ای تی"
                //});
                //return l;
                throw new Exception(message);
            }
            return AdUserRespDTo.ToAdUserDTOList(dt);
        }
        public AdUserRespDTo GetUser(string UserID)
        {
            string cmd = String.Format("SELECT Name FirstName, Family LastName, NationalNo NationalCode, " +
                "DocumentName Grade, JobStringName, khedmat_unit Workplace, t1.unit_khedmat, " +
                "shaghel, halat_eshteghal, PersonNo, PersonCode, Email, M.OU ouplace, '' Username, physicalDeliveryOfficeName " +
                "FROM [Accounting].[Staff] t1 " +
                "LEFT JOIN [Accounting].Mapper_LocationServiceUnit_OU M ON M.unit_khedmat = Accounting.ConvertYK(t1.unit_khedmat) " +
                "WHERE t1.PersonCode = '{0}'", UserID), 
                message;
            using var dt = _databaseHelper.ExecuteQuery(cmd, out message);
            if (dt == null || dt.Rows.Count == 0)
            {
                //return new AdUserRespDTo()
                //    {
                //        UserName = "Rabbanih3",
                //        //ID = 1001,
                //        FirstName = "حمید",
                //        LastName = "ربانی",
                //        Grade = "لیسانس",
                //        Major = "کامپیوتر",
                //        NationalCode = "0703938673",
                //        Password = "123456",
                //        Workplace = "ای تی"
                //    };
                throw new Exception(message);
            }
            var t = AdUserRespDTo.ToAdUserDTOList(dt);
            if (t == null || t.Count == 0) return new AdUserRespDTo();
            return t[0];
        }

        public AdUserRespDTo GetUserchangeStatuse(string UserID)
        {
            string cmd = String.Format("SELECT Name FirstName, Family LastName, NationalNo NationalCode, " +
                "DocumentName Grade, JobStringName, khedmat_unit Workplace, t1.unit_khedmat, " +
                "shaghel, halat_eshteghal, PersonNo, PersonCode, Email, M.OU ouplace, " +
                "REPLACE(t1.Email, '@mums.ac.ir', '') UserName, '' ouplace FROM [Accounting].[VWStaff_ChangeStstuse] t1 " +
                "LEFT JOIN [Accounting].Mapper_LocationServiceUnit_OU M " +
                "ON M.unit_khedmat = t1.unit_khedmat " +
                "WHERE t1.PersonCode = '{0}'", UserID),
                message;
            using var dt = _databaseHelper.ExecuteQuery(cmd, out message);
            if (dt == null || dt.Rows.Count == 0)
            {
                //return new AdUserRespDTo()
                //    {
                //        UserName = "Rabbanih3",
                //        //ID = 1001,
                //        FirstName = "حمید",
                //        LastName = "ربانی",
                //        Grade = "لیسانس",
                //        Major = "کامپیوتر",
                //        NationalCode = "0703938673",
                //        Password = "123456",
                //        Workplace = "ای تی"
                //    };
                throw new Exception(message);
            }
            var t = AdUserRespDTo.ToAdUserDTOList(dt);
            if (t == null || t.Count == 0) return new AdUserRespDTo();
            return t[0];
        }

        public string? GetProposalUserName(string UserID)
        {
            string cmd = String.Format(" EXEC Accounting.GetFindUserName '{0}'", UserID),
                message;
            using var dt = _databaseHelper.ExecuteQuery(cmd, out message);
            if (dt == null || dt.Rows.Count == 0)
            {
                //return new AdUserRespDTo()
                //    {
                //        UserName = "Rabbanih3",
                //        //ID = 1001,
                //        FirstName = "حمید",
                //        LastName = "ربانی",
                //        Grade = "لیسانس",
                //        Major = "کامپیوتر",
                //        NationalCode = "0703938673",
                //        Password = "123456",
                //        Workplace = "ای تی"
                //    };
                throw new Exception(message);
            }
            return dt.Rows[0][0].ToString();
        }

        public bool LoginUser(string UserName, string Password)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                throw new Exception("نام کاربری یا رمز عبور نامعتبر است.");
            if (UserName == "hamid" && Password == "123")
                return true;

            throw new Exception("نام کاربری یا رمز عبور نامعتبر است.");

        }

        public long RegisterLog(LogInfo_Base logInfo)
        {
            string cmd = "INSERT INTO dbo.TbLog_ADController(Level, Message, UserName) " +
                "Values(@Lvl, @Msg, @UName)";
            List<SqlParameter> s = new List<SqlParameter>();
            s.Add(new SqlParameter("@Lvl", logInfo.Level));
            s.Add(new SqlParameter("@Msg", logInfo.Message));
            s.Add(new SqlParameter("@UName", logInfo.UserName));
            _databaseHelper.ExequteNonQuery(cmd, out _, s.ToArray());
            return 0;
        }

        public List<ReportDTO> GetReports()
        {
            string sql = "SELECT [ID], [Level], [Message], [Result], [ErrorMessage], [UserName], [RegisterDate] " +
                "FROM [dbo].[TbLog_ADController] ORDER BY ID desc";
            using var dt = _databaseHelper.ExecuteQuery(sql, out string message);

            if (dt != null)
                return ReportDTO.ToAdUserDTOList(dt); ;
            throw new Exception(message);
        }
    }
}
