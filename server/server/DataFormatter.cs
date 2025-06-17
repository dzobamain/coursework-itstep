using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public class DataFormatter
{
    public bool IsRegister(in User user)
    {
        if (string.IsNullOrWhiteSpace(user.firstName) ||
            string.IsNullOrWhiteSpace(user.lastName) ||
            string.IsNullOrWhiteSpace(user.middleName))
        {
            return true;
        }

        return false;
    }

    public bool ValidateUserData(in User user)
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
            Console.WriteLine($"Error UserVerifier.ValidateUserData(): {ex}");
            return false;
        }
    }

    public bool ValidatePhonePasswordData(in User user)
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

    public bool FindUserInList(in List<User> allUsers, User inputUser)
    {
        /* Search for a user in the list by phone number and password */
        return allUsers.Exists(u =>
            u.password.Equals(inputUser.password) &&
            u.phoneNumber.Equals(inputUser.phoneNumber));
    }
}
