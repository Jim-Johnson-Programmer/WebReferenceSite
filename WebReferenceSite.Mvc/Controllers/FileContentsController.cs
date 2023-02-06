using Microsoft.AspNetCore.Mvc;
using WebReferenceSite.Mvc.Models.ViewModels;

namespace WebReferenceSite.Mvc.Controllers
{
    public class FileContentsController : Controller
    {
        public IActionResult FileContents(string id)
        {
            FileContentsViewModel fileContentsViewModel = new FileContentsViewModel();

            return View(fileContentsViewModel);
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
