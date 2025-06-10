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
            name = "Veronika",
            password = "123456"
        };
        
        result = userVerifier.FindUserInJson(findUser);
        Console.WriteLine(result ? "User found" : "User not found");

        result = userVerifier.WriteUserToJson(findUser);
        result = userVerifier.FindUserInJson(findUser);
        Console.WriteLine(result ? "User found" : "User not found");
    }
}
