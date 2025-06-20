﻿
namespace WebApplication1.Core
{
    public interface IPasswordHashService
    {
        string HashPassword(string password);
        bool ValidatePassword(string password, string correctHash);
    }
    public class PasswordHashService
    {
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}
