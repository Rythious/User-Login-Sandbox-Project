using System;
using System.Linq;
using System.Text;
using UserApi.DataAbstraction;

namespace UserApi
{
    public class PasswordChecker
    {
        public static bool PasswordIsValid(UserLoginAttempt loginAttempt)
        {
            var userManager = new UserAccountManager();

            var userAccount = userManager.GetByUserName(loginAttempt.UserName);

            if (userAccount == null)
            {
                return false;
            }

            var loginPasswordWithUsersSalt = PasswordHasher.AppendSaltToPassword(Encoding.UTF8.GetBytes(loginAttempt.Password), Convert.FromBase64String(userAccount.Salt));

            var loginAttemptHash = PasswordHasher.HashByteArray(loginPasswordWithUsersSalt);

            return Convert.FromBase64String(userAccount.PasswordHash).SequenceEqual(loginAttemptHash);
        }
    }
}
