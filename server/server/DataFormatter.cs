using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public class DataFormatter
{
    public bool ValidateUserData(User user)
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

    public bool FindUserInList(in List<User> allUsers, User inputUser)
    {
        /* Search for a user in the list by phone number and password */
        return allUsers.Exists(u =>
            u.password.Equals(inputUser.password) &&
            u.phoneNumber.Equals(inputUser.phoneNumber));
    }
}
