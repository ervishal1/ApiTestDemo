using ApiSDemo.Data;
using ApiSDemo.Models;
using ApiSDemo.Models.Blog;
using ApiSDemo.Repositories.Infrastructure;
using System.Linq;

namespace ApiSDemo.Repositories.Repos
{
	public class TagsRepo : GenericRepo<Tags>, ITagsRepo
	{
		public TagsRepo(ApplicationDbContext context) : base(context)
		{
			_Context = context;
		}
		public ApplicationDbContext _Context;
		public bool Update(Tags model)
		{
			var data = _Context.Tags.Where(x => x.Id == model.Id).FirstOrDefault();
			if (data != null)
			{
				data.Title = model.Title;
			}
			_Context.Tags.Update(data);
			return data != null;
		}
	}
}
