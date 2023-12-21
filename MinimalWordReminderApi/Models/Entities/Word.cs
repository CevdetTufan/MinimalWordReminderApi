using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MinimalWordReminderApi.Models.Entities
{
	[Index(nameof(Name), IsUnique = true)]
	public class Word
	{
		public int Id { get; set; }


		[Required, StringLength(maximumLength: 200)]
		public string Name { get; set; }
	}
}
