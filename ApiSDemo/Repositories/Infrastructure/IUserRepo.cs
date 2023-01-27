using ApiSDemo.Models;
using System;
using System.Linq;

namespace ApiSDemo.Repositories.Infrastructure
{
	public interface IUserRepo : IGenericRepo<User>
	{
		public bool Update(User user);
	}
}
