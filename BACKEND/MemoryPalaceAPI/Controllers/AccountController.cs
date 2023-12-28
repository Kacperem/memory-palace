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
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }
        /// <summary>
        ///login and get JWT token 
        /// </summary>
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
