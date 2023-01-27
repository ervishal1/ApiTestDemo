using ApiSDemo.Models;
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

	}
}
