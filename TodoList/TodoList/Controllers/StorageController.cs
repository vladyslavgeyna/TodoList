using Microsoft.AspNetCore.Mvc;
using TodoList.ViewModels;

namespace TodoList.Controllers
{
    public class StorageController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StorageController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult Change(ChangeStorageViewModel changeStorageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Task");
            }
            var a = _httpContextAccessor.HttpContext?.Session.GetString("Storage");
            _httpContextAccessor.HttpContext?.Session.Remove("Storage");
            _httpContextAccessor.HttpContext?.Session.SetString("Storage", changeStorageViewModel.StorageType.ToString());
            var b = _httpContextAccessor.HttpContext?.Session.GetString("Storage");
            return RedirectToAction("Index", "Task");
        }
    }
}
