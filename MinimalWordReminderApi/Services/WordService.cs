using Microsoft.Extensions.Logging;
using MinimalWordReminderApi.Models;
using MinimalWordReminderApi.Models.Entities;
using MinimalWordReminderApi.Repositories;
using System;
using System.Net;

namespace MinimalWordReminderApi.Services
{
	public class WordService : IWordService
	{
		private readonly IWordRepository wordRepository;
		private readonly ILogger<WordService> logger;

		public WordService(IWordRepository wordRepository, ILogger<WordService> logger)
		{
			this.wordRepository = wordRepository;
			this.logger = logger;
		}

		public ApiResultModel<Word> Add(WordPostModel model)
		{
			ApiResultModel<Word> result = new ApiResultModel<Word>();
			try
			{
				bool isExist = wordRepository.IsExist(q => q.Name == model.Name);

				if (isExist)
				{
					result.Message = $"{model.Name} is exist.";
				}
				else
				{
					var entity = wordRepository.Insert(new Word { Name = model.Name });
					wordRepository.SaveChanges();
					result.IsSuccess = entity?.Id > 0;
					result.Message = HttpStatusCode.OK.ToString();
					result.Data = entity;
				}
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, ex, ex.Message);
				throw;
			}

			return result;
		}
	}
}
