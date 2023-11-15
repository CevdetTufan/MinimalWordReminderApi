using System.ComponentModel.DataAnnotations;

namespace MinimalWordReminderApi.Models.Entities
{
	public class Word
	{
        public int Id { get; set; }

        [Required]
        public int WordTypeId { get; set; }

        [Required, StringLength(maximumLength: 200)]
		public string Name { get; set; }

        public virtual WordType WordType { get; set; }
    }
}
