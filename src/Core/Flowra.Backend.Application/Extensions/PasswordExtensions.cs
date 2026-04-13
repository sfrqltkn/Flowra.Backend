using System.Security.Cryptography;

namespace Flowra.Backend.Application.Extensions
{
    public static class PasswordExtensions
    {
        // Okunabilirlik için kafa karıştıran karakterler (I, l, 1, O, 0) çıkarıldı
        private const string LowerCase = "abcdefghijkmnopqrstuvwxyz";
        private const string UpperCase = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        private const string Digits = "23456789";
        private const string SpecialChars = "!@#$*_-+";
        private const string AllChars = LowerCase + UpperCase + Digits + SpecialChars;

        // Identity kurallarına uygun, 10 karakterli güçlü ve rastgele bir şifre üretir
        public static string GenerateSecurePassword(int length = 10)
        {
            if (length < 8) length = 8; 

            var passwordChars = new char[length];
            var randomBytes = new byte[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            passwordChars[0] = LowerCase[randomBytes[0] % LowerCase.Length];
            passwordChars[1] = UpperCase[randomBytes[1] % UpperCase.Length];
            passwordChars[2] = Digits[randomBytes[2] % Digits.Length];
            passwordChars[3] = SpecialChars[randomBytes[3] % SpecialChars.Length];

            for (int i = 4; i < length; i++)
            {
                passwordChars[i] = AllChars[randomBytes[i] % AllChars.Length];
            }

            return ShufflePassword(passwordChars);
        }

        private static string ShufflePassword(char[] password)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                int n = password.Length;
                while (n > 1)
                {
                    byte[] box = new byte[1];
                    do rng.GetBytes(box);
                    while (!(box[0] < n * (Byte.MaxValue / n)));

                    int k = (box[0] % n);
                    n--;

                    (password[n], password[k]) = (password[k], password[n]);
                }
            }
            return new string(password);
        }
    }
}