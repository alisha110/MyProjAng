
using AD_Manager.Layers.Model;
using System.DirectoryServices;

namespace AD_Manager.Layers.ADManagment
{
    public class ADHelper : IADHelper
    {
        private readonly string LDAPConnectionString;
        private readonly string UserName;
        private readonly string Password;
        private readonly ILogger _logger;
        public ADHelper(IConfiguration config, ILogger<ADHelper> logger)
        {
            LDAPConnectionString = config.GetConnectionString("LDAPConnectionString");
            UserName = config.GetConnectionString("LDAPUserName");
            Password = config.GetConnectionString("LDAPPassword");
            _logger = logger;
        }

        /// <summary>
        /// Method used to create an entry to the AD.
        /// Replace the path, username, and password.
        /// </summary>
        /// <returns>DirectoryEntry</returns>
        public DirectoryEntry GetDirectoryEntry(string? LdapConnection)
        {
            //DirectoryEntry entry = new DirectoryEntry(LdapDirectory, 
            //    "histest", "His123!@#", AuthenticationTypes.Secure);
            var LConnection = LdapConnection ?? LDAPConnectionString;
            DirectoryEntry entry = 
                new(LDAPConnectionString, UserName, Password, AuthenticationTypes.Secure);
            return entry;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="fname"></param>
        /// <param name="lname"></param>
        /// <param name="password"></param>
        public DirectoryEntry CreateUser(AdUserDTO userDTO, out string message)
            //string username, string fname, string lname, string password)
        {
            //GetRandom Password
            var t = RandomPassword();

            message = string.Empty;
            var entry = GetDirectoryEntry(userDTO.ouplace);

            ////Create CN
            using (DirectoryEntry newUser = entry.Children.Add("CN=" + userDTO.UserName, "user"))
            {
                if (newUser != null)
                {
                    var fullname = userDTO.FirstName + " " + userDTO.LastName;
                    newUser.Properties["samAccountName"].Value = userDTO.UserName;
                    newUser.Properties["givenName"].Value = userDTO.FirstName;  // first name
                    newUser.Properties["sn"].Value = userDTO.LastName;    // surname = last name
                    newUser.Properties["displayName"].Value = fullname;
                    newUser.Properties["mail"].Value = userDTO.UserName + "@mums.ac.ir";
                    newUser.CommitChanges();

                    //Set Password
                    newUser.Invoke("ChangePassword", new object[] { "", t });
                    newUser.CommitChanges();
                    _logger.LogInformation($"Create User {userDTO.UserName}");
                    message = t;
                    return newUser;
                }
                else
                {
                    message = "User Not Found.";
                    _logger.LogError($"CreateUser : User Not Found. For User {userDTO.UserName}");
                    throw new Exception("User Not Found in Active Directory.");
                }
                return null;
            }
        }

        /// <summary>
        /// دریافت رمز عبور رندم
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string RandomPassword()
        {
            try
            {
                byte[] result = new byte[7];
                for (int index = 0; index < 2; index++)
                {
                    result[index] = (byte)new Random().Next(48, 57);
                }
                for (int index = 2; index < 5; index++)
                {
                    result[index] = (byte)new Random().Next(65, 90);
                }
                for (int index = 5; index < 7; index++)
                {
                    result[index] = (byte)new Random().Next(97, 122);
                }
                return System.Text.Encoding.ASCII.GetString(result);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        public Boolean ActivateUser(string username, out string message)
        {
            message = string.Empty;
            try
            {
                var userEntry = FindUser(username);
                if (userEntry != null)
                {
                    ////var t = 0x0200 | 0x0020 | 0x0002;  //غیر فعال
                    ////var t = 0x0200 | 0x0020;  //فعال 
                    var t = ADSTATUSE.NORMAL_ACCOUNT | ADSTATUSE.PASSWD_NOTREQD;
                    userEntry.Properties["userAccountControl"][0] = t;
                    userEntry.CommitChanges();
                    _logger.LogInformation($"ActivateUser : For User {username}");
                    return true;
                }
                else
                {
                    //message = "User Not Found.";
                    _logger.LogError($"ActivateUser : User Not Found. For User {username}");
                    throw new Exception("User Not Found in Active Directory.");
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                _logger.LogError(ex, $" For User : ", username);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        public Boolean DeactivateUser(string username, out string message)
        {
            message = string.Empty;
            try
            {
                var userEntry = FindUser(username);
                if (userEntry != null)
                {
                    ////var t = 0x0200 | 0x0020 | 0x0002;  //غیر فعال
                    ////var t = 0x0200 | 0x0020;  //فعال 
                    var t = ADSTATUSE.NORMAL_ACCOUNT | ADSTATUSE.PASSWD_NOTREQD | ADSTATUSE.ACCOUNTDISABLE;
                    userEntry.Properties["userAccountControl"][0] = t;
                    userEntry.CommitChanges();
                    _logger.LogInformation($"DeactivateUser : For User {username}");
                    return true;
                }
                else
                {
                   // message = "User Not Found.";
                    _logger.LogError($"DeactivateUser : User Not Found. For User {username}");
                    throw new Exception("User Not Found in Active Directory.");
                }
            }
            catch(Exception ex)
            {
                   message = ex.Message;
                _logger.LogError(ex, $" For User : ", username);
            }
            return false;
        }

        /// <summary>
        /// find user
        /// </summary>
        /// <param name="CN"></param>
        /// <returns>user or null</returns>
        public DirectoryEntry? FindUser(string CN)
        {
            DirectorySearcher searcher = new DirectorySearcher();
            searcher.SearchRoot = GetDirectoryEntry(null);
            searcher.SearchScope = SearchScope.Subtree;
            searcher.Filter = $"(&(objectCategory=person)(objectClass=user)(CN={CN}))";
            var results = searcher.FindAll();
            if (results.Count == 0) return null;
            return results[0].GetDirectoryEntry();
        }

    }
}
