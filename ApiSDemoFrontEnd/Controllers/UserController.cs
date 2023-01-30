using ApiSDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using ApiSDemoFrontEnd.Utility;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ApiSDemoFrontEnd.Controllers
{
	public class UserController : Controller
	{
		public readonly IWebHostEnvironment _webHostEnvironment;

		public UserController(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		[HttpGet]
		public IActionResult Login()
		{
			var  h = HttpContext.Session.Get<UserAuth>("User");
			if (h != null)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserAuth user)
		{

			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri("http://localhost:34602");

			HttpResponseMessage response = await client.PostAsJsonAsync($"api/users/login", user);

			if (response.IsSuccessStatusCode)
			{
				var data = response.Content.ReadAsStringAsync().Result;
				Response res = JsonConvert.DeserializeObject<Response>(data);
				if(res.user != null)
				{
					HttpContext.Session.Set<UserAuth>("User", new UserAuth
					{
						Id= res.user.Id,
						Email= res.user.Email,
					});
					return RedirectToAction("Index", "Home");
				}
			}

			ViewBag.AlertMessage = "Not a Valid User";
			return View();
		}

		[HttpGet]
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login", "User");
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(User user)
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri("http://localhost:34602");
			HttpResponseMessage response = await client.PostAsJsonAsync("api/users/", user);
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Login");
			}
			return View(user);
		}

		[HttpGet]
		public async Task<IActionResult> Profile()
		{
			if (HttpContext.Session.Get<UserAuth>("User") != null)
			{
				var user = HttpContext.Session.Get<UserAuth>("User");
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri("http://localhost:34602");
				var res = await client.GetAsync($"api/users/{user.Id}");
				if (res.IsSuccessStatusCode)
				{
					var result = res.Content.ReadAsStringAsync().Result;
					Response response = JsonConvert.DeserializeObject<Response>(result);
					return View(response.user);
				}
				return NotFound();
			}
			return BadRequest();
		}

		[HttpPost]
		public async Task<IActionResult> Profile(User model)
		{
			if (ModelState.IsValid)
			{

			}

			return View(model);
		}

		public string UploadImage(IFormFile file)
		{
			string FileName = file.FileName;
			string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
			FileName = Guid.NewGuid().ToString() + "-" + FileName ;
			string filePath = Path.Combine(path, FileName);

			using var fs = new FileStream(filePath, FileMode.Create);
			file.CopyTo(fs);

			return FileName;
		}

	}
}
