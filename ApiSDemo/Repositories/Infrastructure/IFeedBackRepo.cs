using ApiSDemo.Models;

namespace ApiSDemo.Repositories.Infrastructure
{
	public interface IFeedBackRepo :IGenericRepo<FeedBack>
	{
		public bool Update(FeedBack user);
	}
}
