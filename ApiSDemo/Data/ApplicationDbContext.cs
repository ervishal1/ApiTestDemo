using ApiSDemo.Models;
using ApiSDemo.Models.Blog;
using Microsoft.EntityFrameworkCore;

namespace ApiSDemo.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<FeedBack> FeedBacks { get; set; }
		public DbSet<Tags> Tags { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<PostCategories> PostCategories{ get; set; }
		public DbSet<PostTags> PostTags { get; set; }

	}
}
