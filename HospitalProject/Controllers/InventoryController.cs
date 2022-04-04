using System.Web.Mvc;

namespace HospitalProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class InventoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Show(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult UpdateLeadger()
        {
            return View();
        }
    }
}
