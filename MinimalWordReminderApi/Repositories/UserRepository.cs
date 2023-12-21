using Microsoft.EntityFrameworkCore;
using MinimalWordReminderApi.Models;
using MinimalWordReminderApi.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalWordReminderApi.Repositories
{
	public class UserRepository : Repository<User, int>, IUserRepository
	{
		private readonly MinimalDbContext context;

		public UserRepository(MinimalDbContext context) : base(context)
		{
			this.context = context;
		}

		public async Task<User> Login(UserLoginPostModel model)
		{
			return await context.Users
				.Where(q => q.Username == model.Username && q.Password == model.Password)
				.FirstOrDefaultAsync();
		}
	}
}
