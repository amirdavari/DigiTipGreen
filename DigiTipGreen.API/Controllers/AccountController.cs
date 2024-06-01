using DigiTipGreen.API.DTOs;
using DigiTipGreen.API.Entities;
using DigiTipGreen.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigiTipGreen.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<User> usermanager;
        private readonly TokenService tokenService;

        public AccountController(UserManager<User> usermanager, TokenService tokenService)
        {
            this.usermanager = usermanager;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await usermanager.FindByNameAsync(loginDto.Username);
            if (user == null || !await usermanager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized();
            }

            var userDto = new UserDto
            {
                Email = user.Email,
                Token = await tokenService.GenerateToken(user)
            };
            return userDto;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new User { UserName = registerDto.Username, Email = registerDto.Email };
            var result = await usermanager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }

            await usermanager.AddToRoleAsync(user, "APerson");

            return StatusCode(201);
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await usermanager.FindByNameAsync(User.Identity.Name);

            return new UserDto
            {
                Email = user.Email,
                Token = await tokenService.GenerateToken(user)
            };
        }
    }
}
