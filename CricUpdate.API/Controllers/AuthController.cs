using CricUpdate.API.Models;
using CricUpdate.API.Models.DTOs;
using CricUpdate.API.Repository;
using CricUpdate.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CricUpdate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly TokenService service;

        public AuthController(IAuthRepository authRepository,TokenService service)
        {
            this.authRepository = authRepository;
            this.service = service;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userDTO)
        {
            userDTO.Username = userDTO.Username.ToLower();

            var IsUserNameExists = await authRepository.GetUserAsync(userDTO.Username);
            if(IsUserNameExists != null)
                return BadRequest("Username already exists");
            
            PasswordService.CreatePasswordHash(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = userDTO.Username,
                Email = userDTO.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedAt = DateTime.UtcNow
            };
            await authRepository.RegisterAsync(user);

            var token = service.CreateToken(user);

            var userResponse = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = token
            };
            return CreatedAtAction(nameof(Register),userResponse);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDTO)
        {
            var user = await authRepository.GetUserAsync(loginDTO.Username);
            if(user == null)
                return Unauthorized("Invalid username");
            if(!PasswordService.VerifyPasswordHash(loginDTO.Password,user.PasswordHash,user.PasswordSalt))
                return Unauthorized("Invalid password");
            var token = service.CreateToken(user);

            var userResponse = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = token
            };
            return Ok(userResponse);
        }
    }

}
