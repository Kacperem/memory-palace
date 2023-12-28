using MemoryPalaceAPI.Models.AccountModels;
using MemoryPalaceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace MemoryPalaceAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        ///register a new user
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">User validation error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }
        /// <summary>
        ///login and get JWT token 
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">Invalid username or password</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
