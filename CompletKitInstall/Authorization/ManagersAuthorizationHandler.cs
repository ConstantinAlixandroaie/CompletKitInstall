using CompletKitInstall.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompletKitInstall.Authorization
{
    public class ManagerAuthorizationHandler :
         AuthorizationHandler<OperationAuthorizationRequirement, IDbObject>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IDbObject resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name !=Constants.DeleteOperationName &&
                requirement.Name != Constants.ApproveOperationName&&
                requirement.Name != Constants.PromoteOperationName)
            {
                return Task.CompletedTask;
            }
            if (context.User.IsInRole(Constants.ManagersRole))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
