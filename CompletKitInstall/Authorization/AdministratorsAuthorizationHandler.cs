using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompletKitInstall.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CompletKitInstall.Authorization
{
    public class AdministratorsAuthorizationHandler: AuthorizationHandler<OperationAuthorizationRequirement, IDbObject>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IDbObject resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Constants.AdministratorsRole))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
