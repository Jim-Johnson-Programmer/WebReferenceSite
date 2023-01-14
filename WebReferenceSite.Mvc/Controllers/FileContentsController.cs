using Microsoft.AspNetCore.Mvc;
using WebReferenceSite.Mvc.Models;

namespace WebReferenceSite.Mvc.Controllers
{
    public class FileContentsController : Controller
    {
        public IActionResult FileContents(string id)
        {
            var aa = id;
            return View();
        }

        [HttpPost]
        public IActionResult FileContents(FileContentsViewModel inputViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(inputViewModel);
            }
            FileContentsViewModel fileViewModel = new FileContentsViewModel();
            return View(fileViewModel);
        }
    }
}
