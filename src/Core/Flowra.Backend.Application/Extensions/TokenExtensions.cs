using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Flowra.Backend.Application.Extensions
{
    public static class TokenExtensions
    {
        //Veriyi JWS Compact Serialization formatına uygun hale getirmek için Base64Url ile paketlemek.
        public static string EncodeToken(string token)
        {
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            return WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
        }
        //JWS Compact Serialization formatın kullanarak Base64Url ile paketlenmiş veriyi orijinal haline getirmek için decode işlemi.       
        public static string DecodeToken(string urlEncodedToken)
        {
            byte[] tokenDecodedBytes = WebEncoders.Base64UrlDecode(urlEncodedToken);
            return Encoding.UTF8.GetString(tokenDecodedBytes);
        }
    }
}
