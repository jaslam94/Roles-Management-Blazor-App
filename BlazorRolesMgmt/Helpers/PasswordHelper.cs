using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlazorRolesMgmt.Helpers
{
    public static class PasswordHelper
    {
        public static string SimpleHashPassword(string password)
        {
            return Hasher.SHA256(Hasher.MD5(password));
        }

        public static string CreateRandomPassword(int length = 12)
        {
            // Create a string of characters, numbers, three special characters that are allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789&?!";
            string digits = "0123456789";
            string specialChars = "&?!_@$#";
            string upperChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();

            // Select one random character at a time from the string and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            if (!chars.Any(x => char.IsDigit(x)))
            {
                chars[length - 1] = digits[random.Next(0, digits.Length)];
            }

            if (!chars.Any(x => specialChars.Contains(x)))
            {
                chars[length - 2] = specialChars[random.Next(0, specialChars.Length)];
            }

            if (!chars.Any(x => char.IsLower(x)))
            {
                chars[length - 3] = lowerChars[random.Next(0, lowerChars.Length)];
            }

            if (!chars.Any(x => char.IsUpper(x)))
            {
                chars[length - 4] = upperChars[random.Next(0, upperChars.Length)];
            }

            return new string(chars);
        }
    }
}
