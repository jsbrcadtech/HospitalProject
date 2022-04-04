using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalProject.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Add()
        {
            return View();
        }
    }
}