using Devlance.Domain.Enums;
using Devlance.Domain.Helpers;
using Devlance.Domain.Interfaces.Services;
using Devlance.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IConfiguration _configuration;
		private readonly JWT _jwt;

		public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IOptions<JWT> jwt)
        {
            _userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
			_jwt = jwt.Value;
		}

		public async Task<AuthModel> RegisterAsync(RegisterModel model)
		{
			if (await _userManager.FindByEmailAsync(model.Email) is not null)
				return new AuthModel { Message = "Email is already registered!" };

			if (await _userManager.FindByNameAsync(model.Username) is not null)
				return new AuthModel { Message = "Username is already registered!" };

			var user = new ApplicationUser
			{
				UserName = model.Username,
				Email = model.Email,
				/*                FirstName = model.FirstName,
								LastName = model.LastName*/
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				var errors = string.Empty;

				foreach (var error in result.Errors)
					errors += $"{error.Description},";

				return new AuthModel { Message = errors };
			}

			if (model.Role == UserRolesEnum.FreeLancer)
			{
				await _userManager.AddToRoleAsync(user, "freelancer");
			}
			else if (model.Role == UserRolesEnum.Client)
			{
				await _userManager.AddToRoleAsync(user, "client");
			}

			var jwtSecurityToken = await CreateJwtToken(user);

			return new AuthModel
			{
				Email = user.Email,
				ExpiresOn = jwtSecurityToken.ValidTo,
				IsAuthenticated = true,
				Role = model.Role,
				Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				Username = user.UserName
			};
		}
		public async Task<AuthModel> LoginAsync(TokenRequestModel model)
		{
			var authModel = new AuthModel();

			// Find the user by email
			var user = await _userManager.FindByEmailAsync(model.Email);

			// Check if user exists and password is correct
			if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
			{
				authModel.Message = "Email or Password is incorrect!";
				return authModel;
			}

			// Create JWT token for user
			var jwtSecurityToken = await CreateJwtToken(user);

			// Get user roles - expecting only one role
			var rolesList = await _userManager.GetRolesAsync(user);

			// Check if roles are assigned
			if (rolesList == null || !rolesList.Any())
			{
				authModel.Message = "User does not have any assigned roles!";
				return authModel;
			}

			// Get the first role (assuming only one role per user in your project)
			var firstRole = rolesList.FirstOrDefault();

			// Validate and map the role to your UserRolesEnum
			if (Enum.TryParse(typeof(UserRolesEnum), firstRole, true, out var roleEnum))
			{
				authModel.Role = (UserRolesEnum)roleEnum; // Assign role enum to authModel
			}
			else
			{
				authModel.Message = "Invalid role assigned to the user!";
				return authModel;
			}

			// Fill the auth model with necessary data
			authModel.IsAuthenticated = true;
			authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
			authModel.Email = user.Email;
			authModel.Username = user.UserName;
			authModel.ExpiresOn = jwtSecurityToken.ValidTo;

			return authModel;
		}
		public async Task<string> AssignUserToRoleAsync(AssignUserToRoleModel model)
		{
			var user = await _userManager.FindByIdAsync(model.UserId);

			if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
				return "Invalid user ID or Role";

			if (await _userManager.IsInRoleAsync(user, model.Role))
				return "User already assigned to this role";

			var result = await _userManager.AddToRoleAsync(user, model.Role);

			return result.Succeeded ? string.Empty : "Sonething went wrong";
		}
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
