using Application.Services.AuthServices;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("auth")]
	public class AuthController : ControllerBase
	{
		private readonly AuthService _authService;
		private readonly JwtService _jwtService;

		public AuthController(AuthService authService, JwtService jwtService)
		{
			_authService = authService;
			_jwtService = jwtService;
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
		{
			var user =await _authService.Authenticate(request.Email, request.Password);
			if (user is null)
				return Unauthorized(new { message = "کاربری پیدا نشد" });

			var token = _jwtService.GenerateToken(user);

			return Ok(new LoginResponceDto
			{
				Token = token,
				UserId = user.Id,
				FullName = user.FullName,
				Email = user.Email,
				Role = user.Role.ToString()
			});
		}
	}
}
