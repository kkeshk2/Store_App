using Microsoft.AspNetCore.Authorization;

namespace Store_App.Helpers
{
    public class HttpContextHelper
    {
        public static int? GetUserId(HttpContext? context)
        {
            string? header = GetHeader(context);
            string? bearer = GetBearer(header);
            string? token = GetToken(bearer);
            int? userId = GetUserId(token);

            return userId;
        }

        private static string? GetHeader(HttpContext? context)
        {
            if (context == null) return null;
            return context.Request.Headers["Authorization"];
        }

        private static string? GetBearer(string? header)
        {
            if (header == null) return null;
            if (header.Contains("Bearer") == false) return null;
            return header[header.IndexOf("Bearer")..];
        }

        private static string? GetToken(string? bearer)
        {
            if (bearer == null) return null;
            return bearer.Split(" ")[1];
        }

        private static int? GetUserId(string? token)
        {
            if (token == null) return null;
            return JWTHelper.GetUserId(token);
        }
    }
}
