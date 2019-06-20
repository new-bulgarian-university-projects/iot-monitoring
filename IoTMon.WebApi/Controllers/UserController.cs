using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTMon.Data;
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
        private readonly ApplicationDbContext dbContext;

        public UserController(IUserService userService, IAuthService jwtAuthService, ApplicationDbContext dbContext)
        {
            this.userService = userService ?? throw new ArgumentNullException("userService");
            this.jwtAuthService = jwtAuthService ?? throw new ArgumentNullException("jwtAuthService");
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext");
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

        [HttpGet("{userId:guid}")]
        public IActionResult GetUser(Guid userId)
        {
            try
            {
                var user = this.dbContext.Users.First(u => u.Id == userId);
                var userDto = new UserDto(user);
                return Ok(userDto);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Server error on getting user with id " + userId);
            }

        }
    }
}
