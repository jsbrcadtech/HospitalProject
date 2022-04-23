using HospitalProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HospitalProject.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly HttpClient _client;

        public AppointmentController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44397");
        }

        // GET: Appointment
        public ActionResult Add()
        {
            var userId = User.Identity.GetUserId();
            var res = _client.GetAsync($"/api/PreScreening/{userId}").Result;
            var preScreening = res.Content.ReadAsAsync<PreScreening>().Result;

            ViewBag.PreScreeningCompleted = true;
            if (preScreening == null || preScreening.CreatedAt < DateTime.Now.Date)
            {
                ViewBag.PreScreeningCompleted = false;
            }

            return View();
        }
    }
}