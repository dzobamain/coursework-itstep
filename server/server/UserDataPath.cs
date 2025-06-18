using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class UsersDataPath
    {
        private string userData;

        public UsersDataPath()
        {
            userData = GetUserDataPath();
        }

        private static string GetUserDataPath()
        {
            if (OperatingSystem.IsWindows())
            {
                return @"users\userData.json"; /* Windows */
            }
            else
            {
                return "users/userData.json"; /* Linux/macOS */
            }
        }

        public string GetPath()
        {
            return userData;
        }
    }

}
