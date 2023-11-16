using Microsoft.AspNetCore.Authorization;
using Store_App.Models.Classes;

namespace Store_App.Helpers
{
    public class ValidUserHandler : AuthorizationHandler<ValidUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidUserRequirement requirement)
        {  
            string? header = GetHeader(context);
            string? bearer = GetBearer(header);
            string? token = GetToken(bearer);
            int? userId = GetUserId(token);
            Account? account = GetAccount(userId);

            if (account != null) context.Succeed(requirement);
            else context.Fail();
            return Task.CompletedTask;
        }

        private static string? GetHeader(AuthorizationHandlerContext context)
        {
            if (context.Resource is not HttpContext AuthContext) return null;
            return AuthContext.Request.Headers["Authorization"];
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

        private static Account? GetAccount(int? userId)
        {
            if (userId == null) return null;
            return Account.accessAccountById((int) userId);
        }
    }
}
