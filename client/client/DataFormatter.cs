using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace client.Register
{
    class DataFormatter
    {
        private User user;

        public void SetUser(User newUser)
        {
            user = newUser;
        }
        public User GetUser()
        {
            return user;
        }

        public bool ValidateUserData()
        {
            try
            {
                /* Check if any name fields are empty */
                if (string.IsNullOrWhiteSpace(user.firstName) ||
                    string.IsNullOrWhiteSpace(user.lastName))
                {
                    return false;
                }

                /* Check phone number (must contain only digits and '+') */
                if (!Regex.IsMatch(user.phoneNumber, @"^\+?\d+$"))
                {
                    return false;
                }

                /* Check password (must be at least 8 characters long) */
                if (user.password.Length < 8)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UserVerifier.ValidateUserData(): {ex}");
                return false;
            }
        }

        public bool ValidatePhonePasswordData()
        {
            try
            {
                /* Check phone number (must contain only digits and '+') */
                if (!Regex.IsMatch(user.phoneNumber, @"^\+?\d+$"))
                {
                    return false;
                }

                /* Check password (must be at least 8 characters long) */
                if (user.password.Length < 8)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UserVerifier.ValidatePhonePasswordData(): {ex}");
                return false;
            }
        }
    }
}
