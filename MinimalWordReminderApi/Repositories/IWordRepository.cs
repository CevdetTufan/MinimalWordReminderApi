using MinimalWordReminderApi.Migrations;
using MinimalWordReminderApi.Models.Entities;

namespace MinimalWordReminderApi.Repositories
{
	public interface IWordRepository: IRepository<Word, int>
	{
		
	}
}
