using System.Data;

namespace AD_Manager.Layers.Model
{
    public class AdUserDTO
    {
        //headers = ["ID", "Name", "LastName", "NationalCode", "Grade", "Workplace"];
        /// <summary>
        ///ای دی
        /// </summary>
        //public long ID { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// مقطع تحصیلی
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// نام شغل
        /// </summary>
        public string JobStringName { get; set; }
        /// <summary>
        /// محل خدمت
        /// </summary>
        public string Workplace { get; set; }
        /// <summary>
        /// بخش خدمت
        /// </summary>
        public string unit_khedmat { get; set; }
        /// <summary>
        /// شاغل 
        /// </summary>
        public bool shaghel { get; set; }
        /// <summary>
        /// حالت اشتغال
        /// </summary>
        public string halat_eshteghal { get; set; }
        /// <summary>
        /// شماره پرسنلی
        /// </summary>
        public string PersonNo { get; set; }
        /// <summary>
        /// کد پرسنلی
        /// </summary>
        public string PersonCode { get; set; }
        /// <summary>
        /// رشته تحصیلی
        /// </summary>
        public string Major { get; set; }
        /// <summary>
        /// نام کاربری
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// رمز عبور
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// ou
        /// </summary>
        public string ouplace { get; set; }

        /// <summary>
        /// محل فیزیکی خدمت
        /// </summary>
        public string physicalDeliveryOfficeName { get; set; }

        public IEnumerable<String> GetNames(Object obj, string nameProperty = "Name")
        {
            var type = obj.GetType();
            var property = type.GetProperty(nameProperty);
            yield return property.GetValue(obj, null) as string;
        }

        public static void SetPropertyValue(object instance, string strPropertyName, object newValue)
        {
            Type type = instance.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(strPropertyName);
            propertyInfo.SetValue(instance, newValue, null);
        }

        public static AdUserDTO? ToAdUserDTO(DataRow dr)
        {
            try
            {
                var row = new AdUserDTO();
                
                foreach (var item in dr.ItemArray)
                {
                    //SetPropertyValue(row, )
                }
                //{
                //    //ID = dr["ID"].ToLong(),
                //    FirstName = dr["FirstName"].ToString(),
                //    LastName = dr["LastName"].ToString(),
                //    Grade = dr["Grade"].ToString(),
                //    Major = dr["Major"].ToString(),
                //    NationalCode = dr["NationalCode"].ToString(),
                //    Workplace = dr["Workplace"].ToString(),
                //    UserName = dr["UserName"].ToString(),
                //    Password = dr["Password"].ToString(),
                //    halat_eshteghal = dr[""].ToString(),
                //};
                return row;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<AdUserDTO> ToAdUserDTOList(DataTable dt)
        {
            List<AdUserDTO> list = new List<AdUserDTO>();
            foreach (DataRow dr in dt.Rows)
            {
                var userDTO = ToAdUserDTO(dr);
                if (userDTO != null)
                    list.Add(userDTO);
            }
            return list;
        }


    }

    public class AdUserReqDTo: AdUserDTO
    {
        public static AdUserReqDTo? ToAdUserDTO(DataRow dr)
        {
            try
            {
                var t = dr[0];
                foreach (var item in dr.ItemArray)
                {
                    //SetPropertyValue(row, )
                }

                var row = new AdUserReqDTo()
                {
                    //ID = dr["ID"].ToLong(),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    Grade = dr["Grade"].ToString(),
                    Major = dr["Major"].ToString(),
                    NationalCode = dr["NationalCode"].ToString(),
                    Workplace = dr["Workplace"].ToString()
                };
                return row;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<AdUserReqDTo> ToAdUserDTOList(DataTable dt)
        {
            List<AdUserReqDTo> list = new List<AdUserReqDTo>();
            foreach (DataRow dr in dt.Rows)
            {
                var userDTO = ToAdUserDTO(dr);
                if (userDTO != null)
                    list.Add(userDTO);
            }
            return list;
        }

    }
    public class AdUserRespDTo: AdUserDTO
    {
        public static AdUserRespDTo? ToAdUserDTO(DataRow dr)
        {
            try
            {
                //var t = dr[0];
                var row = new AdUserRespDTo();
                int j = dr.Table.Rows.Count;
                for (int i = 0; i < j; i++)
                {
                    MyExtentions.SetPropertyValue(row, dr.Table.Columns[i].ColumnName, dr[i]);
                }

                //{
                //    //ID = dr["ID"].ToLong(),
                //    FirstName = dr["FirstName"].ToString(),
                //    LastName = dr["LastName"].ToString(),
                //    Grade = dr["Grade"].ToString(),
                //    Major = dr["Major"].ToString(),
                //    NationalCode = dr["NationalCode"].ToString(),
                //    Workplace = dr["Workplace"].ToString()
                //};
                return row;
            }
            catch (Exception edx)
            {
                return null;
            }
        }

        public static List<AdUserRespDTo> ToAdUserDTOList(DataTable dt)
        {

            List<AdUserRespDTo> _List = new List<AdUserRespDTo>();
            _List = (from DataRow dr in dt.Rows
                     select new AdUserRespDTo()
                     {
                         FirstName = dr["FirstName"].ToString(),
                         LastName = dr["LastName"].ToString(),
                         NationalCode = dr["NationalCode"].ToString(),
                         Grade = dr["Grade"].ToString(),
                         JobStringName = dr["JobStringName"].ToString(),
                         unit_khedmat = dr["unit_khedmat"].ToString(),
                         Workplace = dr["Workplace"].ToString(),
                         shaghel = dr["shaghel"].ToBool(),
                         halat_eshteghal = dr["halat_eshteghal"].ToString(),
                         PersonNo = dr["PersonNo"].ToString(),
                         PersonCode = dr["PersonCode"].ToString(),
                         Email = dr["Email"].ToString(),
                         UserName = dr["UserName"].ToString(),
                         ouplace = dr["ouplace"].ToString(),

                         physicalDeliveryOfficeName = dr.Table.Columns.Contains("physicalDeliveryOfficeName") ?
                                                        dr["physicalDeliveryOfficeName"].ToString() : ""

                     }).ToList();

            //List<AdUserRespDTo> list = new List<AdUserRespDTo>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    var userDTO = ToAdUserDTO(dr);
            //    if (userDTO != null)
            //        list.Add(userDTO);
            //}
            return _List;
        }

    }
}
