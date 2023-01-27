using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSDemo.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Email { get; set; }

		[Required,MinLength(6, ErrorMessage ="Six Char is Required")]
		public string Password { get; set; }

		[DataType("date")]
		public DateTime Dob { get; set; }

		public string ImageUri { get; set; }
		[NotMapped]
		public IFormFile FileUpload { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public ICollection<FeedBack> FeedBack { get; set; } = new HashSet<FeedBack>();
	}
}
