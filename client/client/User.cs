public class User
{
    public const string status = "client";
    public string regOrLog { get; set; } = string.Empty; /* only "reg" or "log" */
    public string firstName { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public string middleName { get; set; } = string.Empty;
    public string phoneNumber { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
}
