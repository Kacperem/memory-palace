using Microsoft.AspNetCore.Authorization;

namespace MemoryPalaceAPI.Authorization
{
    public class TwoDigitSystemRequirement : IAuthorizationRequirement
    {
        public TwoDigitSystemRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
        public ResourceOperation ResourceOperation { get; }
    }
}
