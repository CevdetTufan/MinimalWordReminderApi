using MinimalWordReminderApi.Models;
using MinimalWordReminderApi.Models.Entities;

namespace MinimalWordReminderApi.Services
{
	public interface IWordService
	{
		ApiResultModel<Word> Add(WordPostModel model);
	}
}
