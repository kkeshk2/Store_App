using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Store_App.Exceptions;

namespace Store_App.Helpers
{
    public class HttpContextHelper : IHttpContextHelper
    {
        public int GetAccountId(HttpContext? context)
        {
            if (context is null)
            {
                throw new UnauthorizedException("User not authorized to access this resouce.");
            }
       
            string header = GetHeader(context);
            string bearer = GetBearer(header);
            string token = bearer.Split(" ")[1];
            int accountId = GetAccountId(token);

            return accountId;
        }

        private string GetHeader(HttpContext context)
        {
            var header = context.Request.Headers["Authorization"].ToString();
            
            if (string.IsNullOrEmpty(header))
            {
                throw new UnauthorizedException("User not authorized to access this resouce.");
            }

            return header;
        }

        private string GetBearer(string header)
        {
            var index = header.IndexOf("Bearer");

            if (index == -1)
            {
                throw new UnauthorizedException("User not authorized to access this resouce.");
            }

            return header[index..];
        }

        private int GetAccountId(string token)
        {
            IJWTHelper helper = new JWTHelper();
            return helper.GetAccountId(token);
        }

    }
}