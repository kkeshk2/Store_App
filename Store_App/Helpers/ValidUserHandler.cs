using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Store_App.Models.Classes;

namespace Store_App.Helpers
{
    public class ValidUserHandler : AuthorizationHandler<ValidUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidUserRequirement requirement)
        {
            HttpContext? httpContext = GetHttpContext(context);
            int? userId = HttpContextHelper.GetUserId(httpContext);
            Account? account = GetAccount(userId);

            if (account != null) context.Succeed(requirement);
            else context.Fail();
            return Task.CompletedTask;
        }

        private static HttpContext? GetHttpContext(AuthorizationHandlerContext context)
        {
            if (context.Resource is HttpContext httpContext) return httpContext;
            return null;
        }

        private static Account? GetAccount(int? userId)
        {
            if (userId == null) return null;
            return Account.accessAccountById((int) userId);
        }
    }
}
