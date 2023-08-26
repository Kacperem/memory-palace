using MemoryPalaceAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MemoryPalaceAPI.Authorization
{
    public class UserRequirementHandler : AuthorizationHandler<UserRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement, User user)
        {
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (user.Id == int.Parse(userId))
            {
                context.Succeed(requirement);
            }
            var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
