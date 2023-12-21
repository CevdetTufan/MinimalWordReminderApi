using MinimalWordReminderApi.Models.Entities;

namespace MinimalWordReminderApi.Repositories
{
	public class WordRepository : Repository<Word, int>, IWordRepository
	{
		public WordRepository(MinimalDbContext context) : base(context)
		{
		}
	}
}
