using System.ComponentModel.DataAnnotations;

namespace ApiSDemo.Models
{
	public class UserAuth
	{
		public int Id { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
