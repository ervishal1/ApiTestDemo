using System.Collections.Generic;

namespace ApiSDemo.Models
{
	public class Response
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }

		public User user { get; set; }
		public List<User> users { get; set; }

	}
}
