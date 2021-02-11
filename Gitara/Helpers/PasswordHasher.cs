using System;
using System.Security.Cryptography;

namespace Gitara.Helpers
{
    internal static class PasswordHasher
    {
        public static string Hash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            var hash = pbkdf2.GetBytes(20);
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool DoesPasswordMatchHash(string password, string dbHash)
        {
            var hashBytes = Convert.FromBase64String(dbHash);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            var hash = pbkdf2.GetBytes(20);
            try
            {
                for (var i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hash[i])
                        throw new UnauthorizedAccessException();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}