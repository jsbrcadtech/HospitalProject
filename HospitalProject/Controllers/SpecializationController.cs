using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using HospitalProject.Models;
using System.Linq;
using HospitalProject.Models.Dto;

namespace HospitalProject.Controllers
{
    public class SpecializationController : Controller
    {
        private static readonly HttpClient client;
        private ApplicationDbContext _db = new ApplicationDbContext();


        static SpecializationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/");
        }

        public ActionResult Index(string search, string option)
        {
            //string url = "api/specialization";
            //HttpResponseMessage response = client.GetAsync(url).Result;

            //IEnumerable<Specialization> specializations = response.Content.ReadAsAsync<IEnumerable<Specialization>>().Result;

            List<Specialization> specializations = _db.Specializations.ToList();

            //return View(specializations);

            if (option == "Id")
            {
                return View(_db.Specializations.Where(s => s.Id.ToString() == search));

            }
            else if (option == "Name")
            {
                return View(_db.Specializations.Where(s => s.Name.Contains(search)));

            } else 
            {
                //return View(specializations);
                return View(specializations);
            }

        }

        // GET: /Specialization/SearchSpecialization
        public ActionResult SearchSpecialization(string searchKey = null)
        {
            //return View();
            if (searchKey == null)
            {
                return View();
            }
            else
            {
                return View(_db.Specializations.Where(s => s.Name.Contains(searchKey)));
            }
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
