using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class UserDataPath
    {
        private string userData;

        public UserDataPath()
        {
            userData = GetUserDataPath();
        }

        private static string GetUserDataPath()
        {
            if (OperatingSystem.IsWindows())
            {
                return @"user\userData.json"; /* Windows */
            }
            else
            {
                return "user/userData.json"; /* Linux/macOS */
            }
        }

        public string GetPath()
        {
            return userData;
        }
    }

}
