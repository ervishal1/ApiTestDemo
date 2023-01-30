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
	public class TagsController : ControllerBase
	{
		private readonly IUnitOfWork _Uof;

		public TagsController(IUnitOfWork uof)
		{
			_Uof = uof;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			try
			{
				var res = _Uof.Tags.GetAll().ToList();
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
				var res = _Uof.Tags.GetById(id);
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
				var res = _Uof.Tags.GetById(id);
				if (res != null)
				{
					_Uof.Tags.Delete(res);
					_Uof.Save();
					return (new Response { Message = "Tag Deleted Successfully!", StatusCode = 200 });
				}
				return (new Response { Message = "Something Wrong Tag Not Found", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return (new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}

		[HttpPost]
		[Route("")]
		public Response Create(Tags tag)
		{
			try
			{
				var res = _Uof.Tags.Create(tag);
				_Uof.Save();
				if (res)
				{
					return (new Response { Message = "Tag Created Successfully!", StatusCode = 200 });
				}
				return (new Response { Message = "Something Wrong Tag Not Created", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return (new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}

		[HttpPut]
		[Route("{id}")]
		public Response Update(Tags Tag)
		{
			try
			{
				var FindTag = _Uof.Tags.GetById(Tag.Id);
				if (FindTag == null)
				{
					return (new Response { Message = "Tag Not Found!", StatusCode = 400 });
				}

				var res = _Uof.Tags.Update(Tag);
				_Uof.Save();
				if (res)
				{
					return (new Response { Message = "Tag Updated Successfully!", StatusCode = 200 });
				}
				return (new Response { Message = "Something Wrong Tag Not Updated", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return (new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}
	}
}
