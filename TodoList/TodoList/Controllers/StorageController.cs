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
            //var a = _httpContextAccessor.HttpContext?.Session.GetString("Storage");
            HttpContext.Response.Cookies.Delete(StorageCookieHelper.CookieName);
            HttpContext.Response.Cookies.Append(StorageCookieHelper.CookieName, changeStorageViewModel.StorageType.ToString());
            //var b = _httpContextAccessor.HttpContext?.Session.GetString("Storage");
            return RedirectToAction("Index", "Task");
        }
    }
}
