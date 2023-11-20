namespace Utilities.Common
{
    public class HandleHashes
    {
        public static string EndcodePwd(string pwd)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(pwd);
        }

        public static bool VerifyPwd(string pwd, string hashedPwd)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(pwd, hashedPwd);
        }
    }
}