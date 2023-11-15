using Microsoft.EntityFrameworkCore;
using MinimalWordReminderApi.Models.Entities;

namespace MinimalWordReminderApi
{
	public class MinimalDbContext: DbContext
	{
        public MinimalDbContext()
        {
            
        }
        public MinimalDbContext(DbContextOptions<MinimalDbContext> options):base(options) { }

		public DbSet<Word> Words { get; set; }
		public DbSet<WordType> WordTypes { get; set; }
	}
}
