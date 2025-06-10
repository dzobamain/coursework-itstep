using System;

class Server
{
    static void Main(string[] args)
    {
        UserVerifier userVerifier = new UserVerifier();
        User user;
        bool result;

        user = new User
        {
            name = "Veronika",
            password = "12345"
        };

        userVerifier.SetUser(user);
        result = userVerifier.WriteUserToJson(userVerifier.GetUser());

        user = new User
        {
            name = "Volodymyr",
            password = "01234"
        };

        userVerifier.SetUser(user);
        result = userVerifier.WriteUserToJson(userVerifier.GetUser());
    }
}
