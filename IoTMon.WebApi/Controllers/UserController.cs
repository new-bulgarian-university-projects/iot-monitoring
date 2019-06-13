using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTMon.DataServices.Contracts;
using IoTMon.Models.DTO;
using IoTMon.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IoTMon.WebApi.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthService jwtAuthService;

        public UserController(IUserService userService, IAuthService jwtAuthService)
        {
            this.userService = userService ?? throw new ArgumentNullException("userService");
            this.jwtAuthService = jwtAuthService ?? throw new ArgumentNullException("jwtAuthService");
        }

        [HttpPost("signin")]
        public IActionResult Login([FromBody] LoginDto user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var userDto = this.userService.Authenticate(user);

            if (userDto == null)
            {
                return Unauthorized();
            }

            string token = this.jwtAuthService.GenerateToken(userDto);
            return Ok(new { Token = token });
        }

        [HttpPost("signup")]
        public IActionResult Register([FromBody] RegisterDto user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            try
            {
                var createdUser = this.userService.Create(user);
                return this.Ok(createdUser);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Server error !");
            }
        }
    }
}
