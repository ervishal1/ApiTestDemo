using ApiSDemo.Data;
using ApiSDemo.Repositories.Infrastructure;

namespace ApiSDemo.Repositories.Repos
{
	public class UnitOfWork : IUnitOfWork
	{
		public IUserRepo Users { get; private set; }
		public IFeedBackRepo FeedBacks { get; private set; }
		public IPostRepo Posts{ get; private set; }
		public ICategoriesRepo Categories{ get; private set; }
		public ITagsRepo Tags { get; private set; }

		public UnitOfWork(ApplicationDbContext context)
		{
			this.context = context;
			Users = new UserRepo(context);
			FeedBacks = new FeedBackRepo(context);
			Posts = new PostRepo(context);
			Categories = new CategoriesRepo(context);
			Tags = new TagsRepo(context);
		}

		private readonly ApplicationDbContext context;

		public void Save()
		{
			context.SaveChanges();
		}
	}
}
