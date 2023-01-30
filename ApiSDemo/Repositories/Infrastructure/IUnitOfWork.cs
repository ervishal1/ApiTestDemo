namespace ApiSDemo.Repositories.Infrastructure
{
	public interface IUnitOfWork
	{
		IUserRepo Users { get; }
		IFeedBackRepo FeedBacks { get; }
		IPostRepo Posts { get; }
		ITagsRepo Tags { get; }
		ICategoriesRepo Categories { get; }

		void Save();
	}
}
