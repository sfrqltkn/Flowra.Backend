using Flowra.Backend.Application.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace Flowra.Backend.Application.Extensions
{
    public static class GuardExtensions
    {
        // Nesne null ise NotFoundException fırlatır.
        // Genellikle DB'den veri çekerken kullanılır.
        public static T ThrowIfNull<T>([NotNull] this T? obj, string messageKey)
        {
            if (obj is null)
            {
                throw new NotFoundException(messageKey);
            }
            return obj;
        }

        // Nesne null DEĞİLSE ConflictException fırlatır.
        // Genellikle create işlemlerinde unique alan kontrolü için kullanılır.
        public static void ThrowIfExists<T>(this T? obj, string messageKey)
        {
            if (obj is not null)
            {
                throw new ConflictException(messageKey);
            }
        }

        // String null veya boş ise BadRequestException fırlatır.
        public static string ThrowIfNullOrEmpty([NotNull] this string? text, string messageKey)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new BadRequestException(messageKey);
            }
            return text;
        }

        // String null veya sadece boşluktan oluşuyorsa BadRequestException fırlatır.
        public static string ThrowIfNullOrWhiteSpace([NotNull] this string? text, string messageKey)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new BadRequestException(messageKey);
            }
            return text;
        }

        // String belirtilen uzunluktan fazlaysa BadRequestException fırlatır.
        public static string ThrowIfTooLong(this string text, int maxLength, string messageKey)
        {
            ArgumentNullException.ThrowIfNull(text);

            if (text.Length > maxLength)
            {
                throw new BadRequestException(messageKey);
            }

            return text;
        }

        /// Sayı negatifse veya sıfırsa BadRequestException fırlatır.
        public static int ThrowIfZeroOrNegative(this int number, string messageKey)
        {
            if (number <= 0)
            {
                throw new BadRequestException(messageKey);
            }
            return number;
        }

        // Sayı negatifse BadRequestException fırlatır.
        public static decimal ThrowIfNegative(this decimal number, string messageKey)
        {
            if (number < 0)
            {
                throw new BadRequestException(messageKey);
            }
            return number;
        }

        // Liste boşsa (Count == 0) veya null ise BadRequestException fırlatır.
        public static IEnumerable<T> ThrowIfEmpty<T>([NotNull] this IEnumerable<T>? collection, string messageKey)
        {
            if (collection == null || !collection.Any())
            {
                throw new BadRequestException(messageKey);
            }
            return collection;
        }

        // Verilen koşul TRUE ise BusinessRuleException fırlatır.
        // Örn: Kullanıcı zaten aktifse tekrar aktifleştirilemez.
        public static void ThrowIfTrue(this bool condition, string messageKey)
        {
            if (condition)
            {
                throw new BusinessRuleException(messageKey);
            }
        }

        // Verilen koşul FALSE ise BusinessRuleException fırlatır.
        // Örn: İşlem yapmak için bakiye > 0 olmalıdır.
        public static void ThrowIfFalse(this bool condition, string messageKey)
        {
            if (!condition)
            {
                throw new BusinessRuleException(messageKey);
            }
        }

        public static void ThrowIfFailed(this IdentityResult result, string defaultMessage)
        {
            if (result.Succeeded) return;

            var errorDictionary = result.Errors
                .GroupBy(e => e.Code)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.Description).ToArray()
                );

            if (!errorDictionary.Any())
            {
                errorDictionary.Add("General", new[] { defaultMessage });
            }

            throw new AppValidationException(defaultMessage, errorDictionary);
        }


        // Koşul sağlanmıyorsa (örn: kullanıcı ID'leri eşleşmiyorsa) ForbiddenException (403) fırlatır.
        public static void ThrowIfForbidden(this bool isAccessAllowed, string messageKey = "You do not have permission to access this resource.")
        {
            if (!isAccessAllowed)
            {
                throw new ForbiddenException(messageKey);
            }
        }

        // Genel amaçlı koşul kontrolü. Predicate True dönerse belirtilen exception'ı fırlatır.
        public static T ThrowIf<T, TException>(this T obj, Func<T, bool> predicate, string messageKey)
            where TException : AppException
        {
            if (predicate(obj))
            {
                // Reflection ile exception oluşturuluyor (biraz yavaştır, sadece özel durumlarda kullan)
                var exception = (TException)Activator.CreateInstance(typeof(TException), messageKey)!;
                throw exception;
            }
            return obj;
        }


        // Task<T> null ise NotFound fırlatır ve T döner (Service'de await kullanmadan akış sağlar)
        public static async Task<T> ThrowIfNullAsync<T>(this Task<T?> task, string messageKey)
        {
            var result = await task;
            return result.ThrowIfNull(messageKey); // Yukarıdaki senkron metodu kullanır
        }

        // Task<bool> True dönerse CONFLICT fırlatır. (Exists kontrolleri için)
        public static async Task ThrowIfExistsAsync(this Task<bool> task, string messageKey)
        {
            var result = await task;
            if (result)
            {
                throw new ConflictException(messageKey);
            }
        }

        // Task<bool> True dönerse BusinessRule fırlatır.
        public static async Task ThrowIfTrueAsync(this Task<bool> task, string messageKey)
        {
            var result = await task;
            result.ThrowIfTrue(messageKey); // Yukarıdaki senkron metodu kullanır
        }


        //  AUTH & TOKEN GUARDLARI (UnauthorizedException - 401)

        // Nesne (Token) null ise UnauthorizedException fırlatır.
        // Amaç: "Böyle bir token yok" demek yerine "Yetkisiz erişim" diyerek güvenliği sağlamak.
        public static T ThrowIfNullUnauthorized<T>([NotNull] this T? obj, string messageKey)
        {
            if (obj is null)
            {
                throw new UnauthorizedException(messageKey);
            }
            return obj;
        }

        // Koşul FALSE ise (örn: Token.IsActive == false) UnauthorizedException fırlatır.
        public static void ThrowIfInactiveUnauthorized(this bool isActive, string messageKey)
        {
            if (!isActive)
            {
                throw new UnauthorizedException(messageKey);
            }
        }

        // Try-Catch bloklarını azaltmak için Func çalıştıran guard
        // JWT okuma işlemleri gibi hassas yerlerde hata olursa direkt Unauthorized fırlatır.
        public static T ExecuteJwtAction<T>(Func<T> action, string errorMessageKey)
        {
            try
            {
                return action();
            }
            catch
            {
                throw new UnauthorizedException(errorMessageKey);
            }
        }

        // Dosya belirtilen yolda yoksa InternalServerException (500) fırlatır.
        // Genellikle konfigürasyon dosyaları veya email şablonlarını kontrol etmek için kullanılır.
        public static string ThrowIfFileNotExists([NotNull] this string? path, string messageKey)
        {
            // Önce path string'i boş mu diye bakar, sonra dosya var mı diye bakar.
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                // Dosya yoksa bu sunucu taraflı bir eksikliktir, o yüzden InternalServerException fırlatılır.
                throw new InternalServerException(messageKey);
            }
            return path;
        }
    }
}