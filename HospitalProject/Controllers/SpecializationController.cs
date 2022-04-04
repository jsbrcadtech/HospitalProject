using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    public class SpecializationController : Controller
    {
        private static readonly HttpClient client;

        static SpecializationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/");
        }

        public ActionResult Index()
        {
            string url = "api/specialization";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<Specialization> specializations = response.Content.ReadAsAsync<IEnumerable<Specialization>>().Result;

            return View(specializations);
        }


        public ActionResult Details(int id)
        {
            string url = "api/specialization/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Specialization specialization = response.Content.ReadAsAsync<Specialization>().Result;

            return View(specialization);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Specialization specialization)
        {
            string url = "api/specialization";
            HttpResponseMessage response = client.PostAsJsonAsync(url, specialization).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            string url = "api/specialization/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Specialization selectedSpecialization = response.Content.ReadAsAsync<Specialization>().Result;

            return View(selectedSpecialization);
        }

        public ActionResult Update(Specialization specialization)
        {
            string url = "api/specialization/" + specialization.Id;
            HttpResponseMessage response = client.PutAsJsonAsync(url, specialization).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Delete(int id)
        {
            string url = "api/specialization/" + id;
            HttpResponseMessage response = client.DeleteAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
