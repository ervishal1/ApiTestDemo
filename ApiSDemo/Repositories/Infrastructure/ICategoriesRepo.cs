using ApiSDemo.Models.Blog;

namespace ApiSDemo.Repositories.Infrastructure
{
	public interface ICategoriesRepo : IGenericRepo<Category>
	{
		public bool Update(Category model);
	}
}
