using ApiSDemo.Data;
using ApiSDemo.Models;
using ApiSDemo.Repositories.Infrastructure;
using System.Linq;

namespace ApiSDemo.Repositories.Repos
{
	public class FeedBackRepo : GenericRepo<FeedBack>, IFeedBackRepo
	{
		public FeedBackRepo(ApplicationDbContext context) : base(context)
		{
			_Context = context;
		}
		public ApplicationDbContext _Context;

		public bool Update(FeedBack feed)
		{
			var data = _Context.FeedBacks.Where(x => x.Id == feed.Id).FirstOrDefault();
			if (data != null)
			{
				data.Subject = feed.Subject;
				data.Message = feed.Message;
			}
			_Context.FeedBacks.Update(data);
			return data != null;
		}
	}
}
