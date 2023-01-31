using ApiSDemo.Models;
using ApiSDemo.Repositories.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using ApiSDemo.Services;

namespace ApiSDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUnitOfWork _Uof;
		private readonly IMailService _mailService;

		public UsersController(IUnitOfWork uof, IMailService mailService)
		{
			_Uof = uof;
			_mailService = mailService;
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
					return StatusCode(200, new Response { StatusCode = 200, Message = "Record Found!", user = res });
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
				var res = _Uof.Users.Find(x => (x.Email == user.Email && x.Password == PasswordEncrypt(user.Password))).FirstOrDefault();
				if (res != null)
				{
					return StatusCode(200, new Response { StatusCode = 200, Message = "User Authenticated", user = res });
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
		public IActionResult Delete(int id)
		{
			try
			{
				var res = _Uof.Users.GetById(id);
				if (res != null)
				{
					_Uof.Users.Delete(res);
					_Uof.Save();
					return StatusCode(StatusCodes.Status200OK, new Response { Message = "User Deleted Successfully!", StatusCode = 200 });
				}
				return StatusCode(StatusCodes.Status100Continue, new Response { Message = "Something Wrong User Not Found", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}

		[HttpPost]
		[Route("")]
		public async Task<IActionResult> Create(User user)
		{
			try
			{
				var isExists = _Uof.Users.Find(x => x.Email.Equals(user.Email)).FirstOrDefault();
				if (isExists != null)
				{
					return StatusCode(223, new Response { Message = "User Alrady Exists!", StatusCode = 223 });
				}
				user.Password = PasswordEncrypt(user.Password);
				var res = _Uof.Users.Create(user);
				_Uof.Save();
				if (res)
				{
					MailRequest mail = new MailRequest() {
						ToEmail = user.Email,
						Subject = "Email Verification",
						Body = $"<div>Thanx For Joining in This Blog App</div>" +
						$"<div>Please Verify Your Email id <a href='#'>Click Me</a></div>"
					};

				    await _mailService.SendEmailAsync(mail);
					return StatusCode(201, new Response { Message = "User Created Successfully!", StatusCode = 201 });
				}
				return StatusCode(StatusCodes.Status408RequestTimeout, new Response { Message = "Something Wrong User Not Created", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}

		[HttpPut]
		[Route("{id}")]
		public IActionResult Update(User user)
		{
			try
			{
				var FindUser = _Uof.Users.GetById(user.Id);
				if (FindUser == null)
				{
					return StatusCode(400, new Response { Message = "User Not Found!", StatusCode = 400 });
				}

				var res = _Uof.Users.Update(user);
				_Uof.Save();
				if (res)
				{
					return StatusCode(StatusCodes.Status200OK, new Response { Message = "User Updated Successfully!", StatusCode = 200 });
				}
				return StatusCode(StatusCodes.Status100Continue, new Response { Message = "Something Wrong User Not Updated", StatusCode = 100 });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = "Something Wrong Server Error!", StatusCode = 500 });
			}
		}

		public static string PasswordEncrypt(string password)
		{
			if (string.IsNullOrEmpty(password))
			{
				return null;
			}
			else
			{
				byte[] storedPassword = ASCIIEncoding.ASCII.GetBytes(password);
				string encryptPassword = Convert.ToBase64String(storedPassword);
				return encryptPassword;
			}
		}

		public static string PasswordDecrypt(string password)
		{
			if (string.IsNullOrEmpty(password))
			{
				return null;
			}
			else
			{
				byte[] encryptedPassword = Convert.FromBase64String(password);
				string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
				return decryptedPassword;
			}
		}
	}
}
