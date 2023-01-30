using ApiSDemo.Data;
using ApiSDemo.Models.Blog;
using ApiSDemo.Repositories.Infrastructure;
using System.Linq;

namespace ApiSDemo.Repositories.Repos
{
	public class CategoriesRepo : GenericRepo<Category>, ICategoriesRepo
	{
		public CategoriesRepo(ApplicationDbContext context) : base(context)
		{
			_Context = context;
		}
		public ApplicationDbContext _Context;
		public bool Update(Category model)
		{
			var data = _Context.Categories.Where(x => x.Id == model.Id).FirstOrDefault();
			if (data != null)
			{
				data.Title = model.Title;
				data.Description = model.Description;
				data.PublishedDate= model.PublishedDate;
			}
			_Context.Categories.Update(data);
			return data != null;
		}
	}
}
