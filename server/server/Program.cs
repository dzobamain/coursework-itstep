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
            lastName = "Veronika",
            firstName = "Veronika",
            middleName = "Veronika",
            phoneNumber = "+3801234567890",
            password = "StrongPass123"
        };


        result = userVerifier.FindUserInJson(findUser);
        Console.WriteLine(result ? "User found" : "User not found");

        result = userVerifier.WriteUserToJson(findUser);
        result = userVerifier.FindUserInJson(findUser);
        Console.WriteLine(result ? "User found" : "User not found");

        result = userVerifier.ValidateUserData(findUser);
        Console.WriteLine(result ? "Is valid" : "Is not valid");
    }
}
