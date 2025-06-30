using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Server.Logic.Validation
{
    public class DataFormatter
    {
        public bool ValidateUserData(Models.User user)
        {
            if (user.regOrLog != "reg" && user.regOrLog != "log")
            {
                return false;
            }

            if (user.regOrLog == "reg")
            {
                if (string.IsNullOrWhiteSpace(user.firstName) ||
                    string.IsNullOrWhiteSpace(user.lastName))
                {
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(user.phoneNumber) ||
                string.IsNullOrWhiteSpace(user.password))
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

        public bool ValidateCourierData(Models.Courier courier)
        {
            if (string.IsNullOrWhiteSpace(courier.phoneNumber) ||
                string.IsNullOrWhiteSpace(courier.password))
            {
                return false;
            }

            /* Check phone number (must contain only digits and '+') */
            if (!Regex.IsMatch(courier.phoneNumber, @"^\+?\d+$"))
            {
                return false;
            }

            /* Check password (must be at least 8 characters long) */
            if (courier.password.Length < 8)
            {
                return false;
            }

            return true;
        }

        public bool FindUserInList(in List<Models.User> allUsers, Models.User inputUser)
        {
            if(inputUser.regOrLog == "reg")
            {
                return allUsers.Exists(u =>
                u.phoneNumber == inputUser.phoneNumber);
            }
            return allUsers.Exists(u =>
                u.phoneNumber == inputUser.phoneNumber &&
                u.password == inputUser.password);
        }


        public bool FindCourierInList(in List<Models.Courier> allCouriers, Models.Courier courier)
        {
            /* Search for a user in the list by phone number and password */
            return allCouriers.Exists(u =>
                u.password.Equals(courier.password) &&
                u.phoneNumber.Equals(courier.phoneNumber));
        }
    }
}
