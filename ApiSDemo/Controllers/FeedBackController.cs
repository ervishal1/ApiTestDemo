using ApiSDemo.Models;
using ApiSDemo.Repositories.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiSDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeedBackController : ControllerBase
	{
		private readonly IUnitOfWork _Uof;

		public FeedBackController(IUnitOfWork uof)
		{
			_Uof = uof;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			try
			{
				var res = _Uof.FeedBacks.GetAll().ToList();
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
				var res = _Uof.FeedBacks.GetById(id);
				if (res != null)
				{
					return StatusCode(200, new { Res = new Response { StatusCode = 200, Message = "Record Found!" }, FeedBack = res });
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
				var res = _Uof.FeedBacks.GetById(id);
				if (res != null)
				{
					_Uof.FeedBacks.Delete(res);
					_Uof.Save();
					return (new Response { Message = "FeedBack Deleted Successfully!", StatusCode = 200 });
				}
				return (new Response { Message = "Something Wrong FeedBack Not Found", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return (new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}

		[HttpPost]
		[Route("")]
		public Response Create(FeedBack feed)
		{
			try
			{
				var res = _Uof.FeedBacks.Create(feed);
				_Uof.Save();
				if (res)
				{
					return (new Response { Message = "FeedBack Add Successfully!", StatusCode = 200 });
				}
				return (new Response { Message = "Something Wrong FeedBack Faild!", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return (new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}

		[HttpPut]
		[Route("{id}")]
		public Response Update(FeedBack feed)
		{
			try
			{
				var FindFeed = _Uof.FeedBacks.GetById(feed.Id);
				if (FindFeed == null)
				{
					return (new Response { Message = "User Not Found!", StatusCode = 400 });
				}

				var res = _Uof.FeedBacks.Update(feed);
				_Uof.Save();
				if (res)
				{
					return (new Response { Message = "Your FeedBack Updated Successfully!", StatusCode = 200 });
				}
				return (new Response { Message = "Something Wrong FeedBack Not Updated", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return (new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}
	}
}
