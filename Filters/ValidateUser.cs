namespace Shopbridge_base.Filters
{
    public class ValidateUser
    {
        public static bool Login(string username, string password)
        {
            if (username == "admin" && password == "admin@123")
                return true;
            else
                return false;
        }
    }
}
