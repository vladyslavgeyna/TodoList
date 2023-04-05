using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoList.ViewModels;

namespace TodoList.Controllers
{
	public class HomeController : Controller
	{
		public HomeController()
		{

		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}