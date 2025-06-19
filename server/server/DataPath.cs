using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class DataPath
    {
        private string userData;
        private string couriserData;

        public DataPath()
        {
            userData = GetUserDataPath();
            couriserData = GetCourierDataPath();
        }

        private static string GetUserDataPath()
        {
            if (OperatingSystem.IsWindows()) /* Windows */
            {
                return @"users\regusers.json"; 
            }
            else /* Linux/macOS */
            {
                return "users/regusers.json"; 
            }
        }

        private static string GetCourierDataPath()
        {
            if (OperatingSystem.IsWindows()) /* Windows */
            {
                return @"couriers\regcouriers.json";
            }
            else /* Linux/macOS */
            {
                return "couriers/regcouriers.json";
            }
        }

        public string GetUsersPath()
        {
            return userData;
        }

        public string GetCouriersPath()
        {
            return couriserData;
        }
    }

}
