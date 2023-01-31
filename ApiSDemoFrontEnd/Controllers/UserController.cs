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
using System.Net;

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
			var h = HttpContext.Session.Get<UserAuth>("User");
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
				if (res.user != null)
				{
					HttpContext.Session.Set<UserAuth>("User", new UserAuth
					{
						Id = res.user.Id,
						Email = res.user.Email,
					});
					if (user.Rememberme)
					{
						Response.Cookies.Append("UserAuth", JsonConvert.SerializeObject(res.user));
					}
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
				var res = response.Content.ReadAsStringAsync().Result;
				Response data = JsonConvert.DeserializeObject<Response>(res);
				if (data.StatusCode == 201)
				{
					return RedirectToAction("Login");
				}
				ViewBag.Alert = "Email Alrady Exists!";
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
				if (string.IsNullOrEmpty(model.ImageUri))
				{
					model.ImageUri = UploadImage(model);
				}
				else
				{
					UploadImage(model);
				}
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri("http://localhost:34602");
				model.FileUpload = null;
				HttpResponseMessage response = await client.PutAsJsonAsync($"api/users/{model.Id}", model);

				if (response.IsSuccessStatusCode)
				{
					var res = await response.Content.ReadAsStringAsync();
					Response data = JsonConvert.DeserializeObject<Response>(res);
					ViewBag.Alert = data.Message;
				}
				else
				{
					ViewBag.Alert = "Not Updated";
				}
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult ChangePassword()
		{
			return View();
		}

		public string UploadImage(User model)
		{
			if (model.FileUpload == null)
			{
				return "";
			}

			try
			{
				if (String.IsNullOrEmpty(model.ImageUri))
				{
					string FileName = model.FileUpload.FileName;
					string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
					FileName = Guid.NewGuid().ToString() + "-" + FileName;
					string filePath = Path.Combine(path, FileName);
					using var fs = new FileStream(filePath, FileMode.Create);
					model.FileUpload.CopyTo(fs);
					return FileName;
				}

				using var fs1 = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, "Images", model.ImageUri), FileMode.Append);
				model.FileUpload.CopyTo(fs1);
				return "";
			}
			catch (Exception ex)
			{
				return "";
			}

		}

	}
}
