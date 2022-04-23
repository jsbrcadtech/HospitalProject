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

        public ActionResult Error()
        {
            return View();
        }

        // GET: Appointment
        public ActionResult Add()
        {
            var userId = User.Identity.GetUserId();
            var res = _client.GetAsync($"/api/PreScreening/{userId}").Result;
            var preScreening = res.Content.ReadAsAsync<PreScreening>().Result;

            ViewBag.PreScreeningCompleted = preScreening.IsValid();

            return View();
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var res = _client.GetAsync($"/api/appointments/{userId}").Result;

            var appointments = res.Content.ReadAsAsync<List<Appointment>>().Result;

            foreach (var appointment in appointments)
            {
                var staffRes = _client.GetAsync($"/api/StaffData/FindStaff/{appointment.StaffId}").Result;
                appointment.Staff = staffRes.Content.ReadAsAsync<Staff>().Result;
            }

            return View(appointments);
        }

        public ActionResult Delete(int id)
        {
            var res = _client.DeleteAsync($"/api/appointments/{id}").Result;

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}