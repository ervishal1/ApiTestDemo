using System.Collections;
using System.Collections.Generic;

namespace ApiSDemo.Models.Blog
{
	public class Tags
	{
		public int Id { get; set; }
		public string Title { get; set; }

		public ICollection<PostTags> PostTags{ get; set; }
	}
}
