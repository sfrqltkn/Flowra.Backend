using System.Globalization;
using System.Text;

namespace Flowra.BackendApplication.Extensions
{
    public static class UserNameExtensions
    {
        private static readonly CultureInfo TurkishCulture = new CultureInfo("tr-TR");
        public static string ToUserName(this string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
                return string.Empty;

            string raw = $"{(firstName ?? "").Trim()}.{(lastName ?? "").Trim()}".ToLower(TurkishCulture);

            string normalized = raw.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalized)
            {
                char mapped = c switch
                {
                    'ı' => 'i',
                    'ğ' => 'g',
                    'ü' => 'u',
                    'ş' => 's',
                    'ö' => 'o',
                    'ç' => 'c',
                    _ => c
                };

                if ((mapped >= 'a' && mapped <= 'z') || mapped == '.')
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(mapped) != UnicodeCategory.NonSpacingMark)
                    {
                        sb.Append(mapped);
                    }
                }
            }

            string result = sb.ToString().Normalize(NormalizationForm.FormC);

            result = result.Replace("..", "."); 
            result = result.Trim('.');           

            return result;
        }
    }
}