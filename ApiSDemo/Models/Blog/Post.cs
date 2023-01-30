using System;
using System.Collections.Generic;

namespace ApiSDemo.Models.Blog
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime PublishedDate { get; set; }
		public PostStatus StatusOfPost { get; set; }
		public bool PostVisibility { get; set; }

		public int UserId { get; set; }
		public User Author { get; set; }

		public ICollection<PostCategories> PostCategories { get; set; } = new HashSet<PostCategories>();
		public ICollection<PostTags> PostTags { get; set; } = new HashSet<PostTags>();

	}

	public enum PostStatus
	{
		Draft = 0,
		Published = 1,
		Scheduled = 2
	}
}
