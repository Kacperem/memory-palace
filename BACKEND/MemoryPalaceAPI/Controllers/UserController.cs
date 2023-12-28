using MemoryPalaceAPI.Models.TwoDigitSystemModels;
using MemoryPalaceAPI.Models.UserModels;
using MemoryPalaceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemoryPalaceAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) {
            _userService = userService;
        }
        /// <summary>
        ///get all Users, only for admins
        /// </summary>>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserDto>> GetAll([FromQuery] UserQuery userQuery)
        {
            var userDtos = _userService.GetAll(userQuery);

            return Ok(userDtos);
        }
        /// <summary>
        ///get User by User ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById([FromRoute] int id)
        {
            var userDto = _userService.GetById(id);
            return Ok(userDto);
        }
    }
}
