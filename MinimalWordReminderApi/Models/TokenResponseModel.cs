using System;

namespace MinimalWordReminderApi.Models
{
	public class TokenResponseModel
	{
        public string AccessToken { get; set; }
        public DateTime ValidateFrom { get; set; }
    }
}
