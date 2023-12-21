using MinimalWordReminderApi.Models;
using MinimalWordReminderApi.Models.Entities;
using System.Threading.Tasks;

namespace MinimalWordReminderApi.Repositories
{
	public interface IUserRepository : IRepository<User, int>
	{
		Task<User> Login(UserLoginPostModel model);
	}
}
