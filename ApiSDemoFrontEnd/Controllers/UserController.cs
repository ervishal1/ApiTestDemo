using ApiSDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiSDemoFrontEnd.Controllers
{
	public class UserController : Controller
	{
		[HttpGet]
		public IActionResult Login()
		{
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
				return RedirectToAction("Index", "Home");
			}
			ViewBag.AlertMessage = "Not a Valid User";
			return View();
		}
	}
}
