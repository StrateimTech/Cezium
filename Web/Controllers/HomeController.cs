using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetMessage()
        {
            string message = "Welcome";
            return new JsonResult(message);
        }
    }
}