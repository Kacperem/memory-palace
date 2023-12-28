using MemoryPalaceAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MemoryPalaceAPI.Authorization
{
    public class TwoDigitSystemRequirementHandler : AuthorizationHandler<TwoDigitSystemRequirement, TwoDigitSystem>
    {
        private readonly MemoryPalaceDbContext _dbContext;
        public TwoDigitSystemRequirementHandler(MemoryPalaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TwoDigitSystemRequirement requirement,
            TwoDigitSystem twoDigitSystem)
        {
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (requirement.ResourceOperation == ResourceOperation.Create)
            {
                var createdTwoDigitSystem = _dbContext.TwoDigitSystems.FirstOrDefault(r => r.CreatedById == int.Parse(userId));
                if (createdTwoDigitSystem == null)
                {
                    context.Succeed(requirement);
                }
            }
            if (requirement.ResourceOperation == ResourceOperation.Read ||
                requirement.ResourceOperation == ResourceOperation.Update ||
                requirement.ResourceOperation == ResourceOperation.Delete)
            {
                if (twoDigitSystem.CreatedById == int.Parse(userId))
                {
                    context.Succeed(requirement);
                }
                var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
                if (role == "Admin")
                {
                    context.Succeed(requirement);
                }
            }
            

            return Task.CompletedTask;
        }
    }
}
