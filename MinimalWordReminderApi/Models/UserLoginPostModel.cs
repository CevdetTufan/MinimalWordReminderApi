namespace MinimalWordReminderApi.Models
{
	public class UserLoginPostModel
	{
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
