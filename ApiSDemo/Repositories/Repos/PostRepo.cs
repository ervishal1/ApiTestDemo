using ApiSDemo.Data;
using ApiSDemo.Models.Blog;
using ApiSDemo.Repositories.Infrastructure;
using System.Linq;

namespace ApiSDemo.Repositories.Repos
{
	public class PostRepo : GenericRepo<Post>, IPostRepo
	{
		public PostRepo(ApplicationDbContext context) : base(context)
		{
			_Context = context;
		}

		public ApplicationDbContext _Context;
		public bool Update(Post model)
		{
			var data = _Context.Posts.Where(x => x.Id == model.Id).FirstOrDefault();
			if (data != null)
			{
				data.Title = model.Title;
			}
			_Context.Posts.Update(data);
			return data != null;
		}
	}
}
