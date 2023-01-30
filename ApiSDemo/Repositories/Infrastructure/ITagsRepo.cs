using ApiSDemo.Models;
using ApiSDemo.Models.Blog;

namespace ApiSDemo.Repositories.Infrastructure
{
	public interface ITagsRepo : IGenericRepo<Tags>
	{
		public bool Update(Tags model);
	}
}
