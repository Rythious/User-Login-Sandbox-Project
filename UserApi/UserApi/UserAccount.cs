using System;
using System.ComponentModel.DataAnnotations;

namespace UserApi
{
    public class UserAccount
    {
        [Key]
        public int UserAccountId { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string HashAlgorithm { get; set; }
        public DateTime LastLogin { get; set; }

        private UserAccount(string userName, string emailAddress)
        {
            UserName = userName;
            Email = emailAddress;
        }

        public UserAccount()
        {
        }

        public static UserAccount FromUserNameAndPassword(string userName, string password, string emailAddress)
        {
            (string passwordHash, string salt) = PasswordHasher.HashPlaintextPasswordWhileAddingSalt(password);

            return new UserAccount(userName, emailAddress)
            {
                LastLogin = DateTime.UtcNow,
                PasswordHash = passwordHash,
                Salt = salt,
                HashAlgorithm = PasswordHasher.HashMethod
            };
        }
    }
}