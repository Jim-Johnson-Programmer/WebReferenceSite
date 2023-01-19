using Microsoft.AspNetCore.Mvc;

namespace WebReferenceSite.Mvc.Controllers
{
    public class FolderNavigatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
