using Microsoft.AspNetCore.Authorization;

namespace MemoryPalaceAPI.Authorization
{

    public class UserRequirement : IAuthorizationRequirement
    {
        public UserRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
        public ResourceOperation ResourceOperation { get; }
    }
}
