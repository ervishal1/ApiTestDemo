using System;
using System.ComponentModel.DataAnnotations;

namespace ApiSDemo.Models
{
	public class FeedBack
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Subject { get; set; }
		[Required]
		public string Message { get; set; }

		[Required]
		public int UserId { get; set; }
		public User User { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
