using ApiSDemo.Models;
using ApiSDemo.Repositories.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUnitOfWork _Uof;

		public UsersController(IUnitOfWork uof)
		{
			_Uof = uof;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			try
			{
				var res = _Uof.Users.GetAll().ToList();
				if (res != null)
					return StatusCode(200, res);

				return StatusCode(204, "No Content Avilable");

			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
			}
		}

		[HttpGet]
		[Route("{id}")]
		public IActionResult GetById(int id)
		{
			try
			{
				var res = _Uof.Users.GetById(id);
				if (res != null)
				{
					return StatusCode(200, new { Res = new Response { StatusCode = 200, Message = "Record Found!" }, User = res });
				}

				return StatusCode(204, new Response { StatusCode = 204, Message = "No Content Avilable" });

			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
			}
		}

		[HttpPost]
		[Route("login")]
		public IActionResult LoginAuth(UserAuth user)
		{
			try
			{
				var res = _Uof.Users.Find(x => (x.Email == user.Email && x.Password == user.Password)).FirstOrDefault();
				if (res != null)
				{
					return Ok(new Response { StatusCode = 200, Message = "User Authenticated" });
				}
				return StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized User");
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpDelete]
		[Route("{id}")]
		public Response Delete(int id)
		{
			try
			{
				var res = _Uof.Users.GetById(id);
				if (res != null)
				{
					_Uof.Users.Delete(res);
					_Uof.Save();
					return (new Response { Message = "User Deleted Successfully!", StatusCode = 200 });
				}
				return (new Response { Message = "Something Wrong User Not Found", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return (new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}

		[HttpPost]
		[Route("")]
		public Response Create(User user)
		{
			try
			{
				var res = _Uof.Users.Create(user);
				_Uof.Save();
				if (res)
				{
					return (new Response { Message = "User Created Successfully!", StatusCode = 200 });
				}
				return (new Response { Message = "Something Wrong User Not Created", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return (new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}

		[HttpPut]
		[Route("{id}")]
		public Response Update(User user)
		{
			try
			{
				var FindUser = _Uof.Users.GetById(user.Id);
				if (FindUser == null)
				{
					return (new Response { Message = "User Not Found!", StatusCode = 400 });
				}

				var res = _Uof.Users.Update(user);
				_Uof.Save();
				if (res)
				{
					return (new Response { Message = "User Updated Successfully!", StatusCode = 200 });
				}
				return (new Response { Message = "Something Wrong User Not Updated", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return (new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}
	}
}
