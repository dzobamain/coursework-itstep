using Newtonsoft.Json;
using System;

class Server
{
    static void Main(string[] args)
    {
        UserVerifier userVerifier = new UserVerifier();
        User findUser;
        bool result;

        findUser = new User
        {
            firstName = "Veronika",
            lastName = "Veronika",
            middleName = "Veronika",
            phoneNumber = "+3801234567890",
            password = "StrongPass123"
        };

        userVerifier.WriteNewUserToJson(findUser);
    }
}
