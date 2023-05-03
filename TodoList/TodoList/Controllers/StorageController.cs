using Microsoft.AspNetCore.Mvc;
using TodoList.Service.Utils;
using TodoList.ViewModels;

namespace TodoList.Controllers
{
    public class StorageController : Controller
    {
        [HttpPost]
        public IActionResult Change(ChangeStorageViewModel changeStorageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Task");
            }
            HttpContext.Response.Cookies.Delete(StorageCookieHelper.CookieName);
            HttpContext.Response.Cookies.Append(StorageCookieHelper.CookieName, changeStorageViewModel.StorageType.ToString());
            return RedirectToAction("Index", "Task");
        }
    }
}
