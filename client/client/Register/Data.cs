using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Register
{
    class Data
    {
        private readonly string dataPath; /* Path to the user data file */
        private User user;

        public UserVerifier()
        {
            dataPath = GetDataPath();
        }
        private static string GetDataPath()
        {
            if (OperatingSystem.IsWindows())
            {
                return @"user\data.json"; /* Windows */
            }
            else
            {
                return "user/data.json"; /* Linux/macOS */
            }
        }
    }
}
