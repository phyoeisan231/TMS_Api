
using AutoMapper;
using TMS_Api.JwtFeatures;
using TMS_Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using TMS_Api.DTOs;

namespace TMS_Api.Controllers
{
	[Route("api/[controller]/[Action]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IMapper _mapper;
		private readonly JwtHandler _jwtHandler;
		private readonly AccountDAL _queryDAL;

		public AccountController(UserManager<IdentityUser> userManager, IMapper mapper, JwtHandler jwtHandler, AccountDAL queryDAL)
		{
			_userManager = userManager;
			_mapper = mapper;
			_jwtHandler = jwtHandler;
			_queryDAL = queryDAL;

		}

		[HttpPost]
		public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
		{
			if (userForRegistration == null || !ModelState.IsValid)
				return BadRequest();

			var user = _mapper.Map<IdentityUser>(userForRegistration);

			var result = await _userManager.CreateAsync(user, userForRegistration.Password);

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);

				return BadRequest(new RegistrationResponseDto { Errors = errors });
			}

			await _userManager.AddToRoleAsync(user, userForRegistration.Role);

			return StatusCode(201);
		}

		
		[HttpPost]
		public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
		{
			var user = await _userManager.FindByEmailAsync(userForAuthentication.Email);

			if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
				return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

			var signingCredentials = _jwtHandler.GetSigningCredentials();
			var claims = await _jwtHandler.GetClaims(user);
			var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
			var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDto {IsAuthSuccessful = true, Token = token });
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
			if (user == null)
				return BadRequest("Invalid Request");

			await _userManager.RemovePasswordAsync(user);
			var resetPassResult = await _userManager.AddPasswordAsync(user, resetPasswordDto.Password);

			if (!resetPassResult.Succeeded)
			{
				var errors = resetPassResult.Errors.Select(e => e.Description);
				return BadRequest(new { Errors = errors });
			}
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetUserRoles()
		{
			DataTable dt = await _queryDAL.GetUserRoles();
			return Ok(dt);
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			DataTable dt = await _queryDAL.GetUsers();
			return Ok(dt);
		}

		
		[HttpDelete]
		public async Task<ResponseMessage> DeleteUser(string id)
		{
			ResponseMessage msg = new ResponseMessage { Status = false };
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				try
				{
					var rolesForUser = await _userManager.GetRolesAsync(user);
					foreach (var item in rolesForUser.ToList())
					{
						// item should be the name of the role
						var result = await _userManager.RemoveFromRoleAsync(user, item);
					}
					var uresult = await _userManager.DeleteAsync(user);

					msg.Status = true;
					msg.MessageContent = "Successfully Removed!";
					
				}
				catch (Exception e)
				{
					msg.MessageContent = e.Message;
				}
			}
			else
			{
				msg.MessageContent = "Something went wrong!";
			}
			return msg;
		}
	}
}
