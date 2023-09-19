using MemoryPalaceAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MemoryPalaceAPI.Authorization
{
    public class TwoDigitSystemRequirementHandler : AuthorizationHandler<TwoDigitSystemRequirement, TwoDigitSystem>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TwoDigitSystemRequirement requirement,
            TwoDigitSystem twoDigitSystem)
        {
            //if (requirement.ResourceOperation == ResourceOperation.Read ||
            //    requirement.ResourceOperation == ResourceOperation.Create)
            if (requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (twoDigitSystem.CreatedById == int.Parse(userId))
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
