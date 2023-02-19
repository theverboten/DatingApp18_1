using System.Globalization;
using System.Security.Claims;
using System.Security.Cryptography;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

         public static string GetUserId(this ClaimsPrincipal user) //patří tam int
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}