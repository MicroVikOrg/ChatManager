namespace ChatManager.WebApi.Helpers
{
    public static class Regexs
    {
        public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$";
        public const string Email = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public const string Username = "^[a-zA-Z_][a-zA-Z0-9_.]*";
    }
}
