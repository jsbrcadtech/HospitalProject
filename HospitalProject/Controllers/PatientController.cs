using HospitalProject.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System.Linq;


namespace HospitalProject.Controllers
{
    public class PatientController : Controller
    {
        private static readonly HttpClient client;
        private ApplicationDbContext _db = new ApplicationDbContext();


        static PatientController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/");
        }

        public ActionResult Index(string search, string option)
        {
            //string url = "api/patient";
            //HttpResponseMessage response = client.GetAsync(url).Result;

            //IEnumerable<Patient> patients = response.Content.ReadAsAsync<IEnumerable<Patient>>().Result;
            List<Patient> patients = _db.Patients.ToList();

            if (option == "Patient")
            {
                return View(_db.Patients.Where(p => p.Name.Contains(search)));
           
            } else if (option == "email")
            {
                return View(_db.Patients.Where(p => p.Email.Contains(search)));

            } else
            {
                return View(patients);

            }



        }


        public ActionResult Details(int id)
        {
            string url = "api/patient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Patient patient = response.Content.ReadAsAsync<Patient>().Result;

            return View(patient);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            string url = "api/patient";
            HttpResponseMessage response = client.PostAsJsonAsync(url, patient).Result;

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
            string url = "api/patient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Patient selectedPatient = response.Content.ReadAsAsync<Patient>().Result;

            return View(selectedPatient);
        }

        public ActionResult Update(Patient patient)
        {
            string url = "api/patient/" + patient.Id;
            HttpResponseMessage response = client.PutAsJsonAsync(url, patient).Result;

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
            string url = "api/patient/" + id;
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
