using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Store_App.Exceptions;
using Store_App.Models.AccountModel;

namespace Store_App.Helpers
{
    public class ValidUserHandler : AuthorizationHandler<ValidUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidUserRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                return HandleRequirement(context, httpContext, requirement);
            }

            context.Fail();
            return Task.CompletedTask;
        }

        private static Task HandleRequirement(AuthorizationHandlerContext context, HttpContext httpContext, ValidUserRequirement requirement)
        {
            try
            {
                IHttpContextHelper helper = new HttpContextHelper();
                int accountId = helper.GetAccountId(httpContext);
                var account = new Account();
                account.AccessAccount(accountId);
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            catch (AccountNotFoundException)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            catch (UnauthorizedException)
            {
                context.Fail();
                return Task.CompletedTask;
            }
        }
    }
}
