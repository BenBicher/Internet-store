using System.Web.Mvc;

namespace MyStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult index()
        {
            return RedirectToAction("Index", "Products");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}