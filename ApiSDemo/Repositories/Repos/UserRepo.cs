using ApiSDemo.Data;
using ApiSDemo.Models;
using ApiSDemo.Repositories.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiSDemo.Repositories.Repos
{
	public class UserRepo : GenericRepo<User>, IUserRepo
	{
		public ApplicationDbContext _Context;
		public UserRepo(ApplicationDbContext context) : base(context)
		{
			_Context = context;
		}

		public bool Update(User user)
		{
			var data = _Context.Users.Where(x => x.Id == user.Id).FirstOrDefault();
			if (data != null)
			{
				data.Name= user.Name;
				data.Email= user.Email;		
				data.Password= user.Password;
				data.ImageUri= user.ImageUri;
				data.Dob = user.Dob;
			}
			_Context.Users.Update(data);
			return data != null;
		}
	}
}
