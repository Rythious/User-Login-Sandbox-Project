using System;
using System.Security.Cryptography;
using System.Text;

namespace UserApi
{
    public class PasswordHasher
    {
        public static string HashMethod => "SHA512";
        public static (string passwordHash, string salt) HashPlaintextPasswordWhileAddingSalt(string plaintextPassword)
        {
            var passwordAsByteArray = Encoding.UTF8.GetBytes(plaintextPassword);

            var salt = new byte[25];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            byte[] passwordAsByteArrayPlusSalt = AppendSaltToPassword(passwordAsByteArray, salt);

            var passwordHash = HashByteArray(passwordAsByteArrayPlusSalt);

            return (Convert.ToBase64String(passwordHash), Convert.ToBase64String(salt));
        }

        public static byte[] HashByteArray(byte[] byteArray)
        {
            var sha512 = new SHA512Managed();

            return sha512.ComputeHash(byteArray);
        }

        public static byte[] AppendSaltToPassword(byte[] passwordArray, byte[] salt)
        {
            byte[] passwordAsByteArrayPlusSalt = new byte[passwordArray.Length + salt.Length];

            for (int i = 0; i < passwordArray.Length; i++)
            {
                passwordAsByteArrayPlusSalt[i] = passwordArray[i];
            }

            for (int i = 0; i < salt.Length; i++)
            {
                passwordAsByteArrayPlusSalt[passwordArray.Length + i] = salt[i];
            }

            return passwordAsByteArrayPlusSalt;
        }
    }
}
