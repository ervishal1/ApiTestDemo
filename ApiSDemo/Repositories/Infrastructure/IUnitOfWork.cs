namespace ApiSDemo.Repositories.Infrastructure
{
	public interface IUnitOfWork
	{
		IUserRepo Users { get; }
		IFeedBackRepo FeedBacks { get; }

		void Save();
	}
}
