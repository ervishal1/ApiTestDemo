using ApiSDemo.Models.Blog;

namespace ApiSDemo.Repositories.Infrastructure
{
	public interface IPostRepo : IGenericRepo<Post>
	{
		public bool Update(Post model);
	}
}
