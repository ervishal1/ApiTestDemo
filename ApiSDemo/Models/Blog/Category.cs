using System;
using System.Collections;
using System.Collections.Generic;

namespace ApiSDemo.Models.Blog
{
	public class Category
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime PublishedDate { get; set; }

		public ICollection<PostCategories> PostCategories{ get; set; } = new HashSet<PostCategories>();
	}
}
