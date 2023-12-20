using MinimalWordReminderApi.Models;
using MinimalWordReminderApi.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalWordReminderApi.Services
{
	public interface IUserService
	{
		Task<TokenResponseModel> Login(UserLoginPostModel model);

		List<User> GetUsers();
	}
}
