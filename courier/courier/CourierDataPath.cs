using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courier
{
    public class CourierDataPath
    {
        private string userData;

        public CourierDataPath()
        {
            userData = GetUserDataPath();
        }

        private static string GetUserDataPath()
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                return @"courier\cdata.json"; /* Windows */
            }
            else
            {
                return "courier/cdata.json"; /* Linux/macOS */
            }
        }

        public string GetPath()
        {
            return userData;
        }
    }

}
