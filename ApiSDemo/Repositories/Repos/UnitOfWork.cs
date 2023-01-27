using ApiSDemo.Data;
using ApiSDemo.Repositories.Infrastructure;

namespace ApiSDemo.Repositories.Repos
{
	public class UnitOfWork : IUnitOfWork
	{
		public IUserRepo Users { get; private set; }
		public IFeedBackRepo FeedBacks { get; private set; }

		public UnitOfWork(ApplicationDbContext context)
		{
			this.context = context;
			Users = new UserRepo(context);
			FeedBacks = new FeedBackRepo(context);
		}

		private readonly ApplicationDbContext context;

		public void Save()
		{
			context.SaveChanges();
		}
	}
}
