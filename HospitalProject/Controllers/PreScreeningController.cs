using System;
using System.Net.Http;
using System.Web.Mvc;
using HospitalProject.Models;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace HospitalProject.Controllers
{
    public class PreScreeningController : Controller
    {

        private static readonly HttpClient client;

        static PreScreeningController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397");
        }

        //GET: Error
        public ActionResult Error()
        {
            return View();
        }

        // GET: PreScreening/New
        public ActionResult New()
        {
            return View();
        }

        // POST: PreScreening/Create
        [HttpPost]
        public ActionResult Create(PreScreening preScreening)
        {
            preScreening.UserId = User.Identity.GetUserId();
            string url = "/api/prescreening";
            HttpResponseMessage response = client.PostAsJsonAsync(url, preScreening).Result;
            Debug.WriteLine(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Add", "Appointment");
            }
            {
                return RedirectToAction("Error");
            }
        }
    }
}