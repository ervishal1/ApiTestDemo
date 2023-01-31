using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiSDemo.Models
{
	public class UserAuth
	{
		public int Id { get; set; }
		[Required]
		public string Email { get; set; }
		[Required,PasswordPropertyText]
		public string Password { get; set; }
		[DisplayName("Remember me")]
		public bool Rememberme { get; set; }
	}
}
