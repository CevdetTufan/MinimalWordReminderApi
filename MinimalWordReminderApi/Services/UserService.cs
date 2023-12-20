using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MinimalWordReminderApi.Models;
using MinimalWordReminderApi.Models.Entities;
using MinimalWordReminderApi.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MinimalWordReminderApi.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository userRepository;
		private readonly IConfiguration configuration;

		public UserService(IUserRepository userRepository, IConfiguration configuration)
		{
			this.userRepository = userRepository;
			this.configuration = configuration;
		}

		public List<User> GetUsers()
		{
			return userRepository.GetAll(q => q.Username != "a").ToList();
		}

		public async Task<TokenResponseModel> Login(UserLoginPostModel model)
		{
			var user = await userRepository.Login(model);

			if (user != null)
			{
				return CreateToken(user);
			}

			return null;
		}

		private TokenResponseModel CreateToken(User user)
		{
			string secretKey = configuration["JwtSettings:Key"];
			string issuer = configuration["JwtSettings:Issuer"];
			string audience = configuration["JwtSettings:Audience"];

			var claims = new[]
			{
				new Claim("UserId",user.Id.ToString()),
				new Claim(ClaimTypes.Name,user.Username)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			DateTime expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["JwtSettings:Duration"]));

			var token = new JwtSecurityToken(
					issuer: issuer,
					audience: audience,
					claims: claims,
					expires: expires,
					signingCredentials: creds
				);

			var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

			return new TokenResponseModel
			{
				AccessToken = tokenString,
				ValidateFrom = expires
			};
		}
	}
}
