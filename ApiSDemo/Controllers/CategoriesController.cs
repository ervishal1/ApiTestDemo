using ApiSDemo.Models;
using ApiSDemo.Models.Blog;
using ApiSDemo.Repositories.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiSDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly IUnitOfWork _Uof;

		public CategoriesController(IUnitOfWork uof)
		{
			_Uof = uof;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			try
			{
				var res = _Uof.Categories.GetAll().ToList();
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
				var res = _Uof.Categories.GetById(id);
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

		[HttpDelete]
		[Route("{id}")]
		public Response Delete(int id)
		{
			try
			{
				var res = _Uof.Categories.GetById(id);
				if (res != null)
				{
					_Uof.Categories.Delete(res);
					_Uof.Save();
					return (new Response { Message = "Category Deleted Successfully!", StatusCode = 200 });
				}
				return (new Response { Message = "Something Wrong Category Not Found", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return (new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}

		[HttpPost]
		[Route("")]
		public Response Create(Category model)
		{
			try
			{
				var res = _Uof.Categories.Create(model);
				_Uof.Save();
				if (res)
				{
					return (new Response { Message = "Category Created Successfully!", StatusCode = 200 });
				}
				return (new Response { Message = "Something Wrong Category Not Created", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return (new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}

		[HttpPut]
		[Route("{id}")]
		public Response Update(Category model)
		{
			try
			{
				var FindUser = _Uof.Categories.GetById(model.Id);
				if (FindUser == null)
				{
					return (new Response { Message = "Category Not Found!", StatusCode = 400 });
				}

				var res = _Uof.Categories.Update(model);
				_Uof.Save();
				if (res)
				{
					return (new Response { Message = "Category Updated Successfully!", StatusCode = 200 });
				}
				return (new Response { Message = "Something Wrong Category Not Updated", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return (new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}
	}
}
