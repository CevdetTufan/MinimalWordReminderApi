using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MinimalWordReminderApi.Models.Entities
{
	public class WordType
	{
		public int Id { get; set; }


		[Required, StringLength(maximumLength: 200)]
		public string Name { get; set; }

		public virtual ICollection<Word> Words { get; set; }
	}
}
