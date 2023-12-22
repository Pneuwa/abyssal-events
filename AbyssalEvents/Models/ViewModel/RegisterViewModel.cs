using System.ComponentModel.DataAnnotations;

namespace Abyssal_Events.Models.ViewModel
{
	public class RegisterViewModel
	{
		[Required]
		public string Username { get; set; }
        [Required]
		[EmailAddress]
        public string Email { get; set; }
        [Required]
		[MinLength(8, ErrorMessage = "Password has to be at least 8 characters")]
        public string Password { get; set; }
	}
}
