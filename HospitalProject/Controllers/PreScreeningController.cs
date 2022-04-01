using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using System.Diagnostics;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System.Web.Script.Serialization;

namespace HospitalProject.Controllers
{
    public class PreScreeningController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PreScreeningController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/");
        }

        // GET: PreScreening
        public ActionResult Index()
        {
            return View();
        }

        //GET: Success
        public ActionResult Success()
        {
            return View();
        }

        //GET: Success
        public ActionResult Failure()
        {
            return View();
        }

        //GET: Error
        public ActionResult Error()
        {
            return View();
        }

        // GET: PreScreening/Details/5
        public ActionResult Details(int id)
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
            //curl -H "Content-Type:application/json" -d @prescreening.json https://localhost:44397/api/PreScreeningData/AddPreScreening
            string url = "PreScreeningData/AddPreScreening";

            string jsonpayload = jss.Serialize(preScreening);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            Debug.WriteLine(preScreening.VaccinationStatus);

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode && (preScreening.VaccinationStatus == 1))
            {
                return RedirectToAction("Success");
            }else if (response.IsSuccessStatusCode && (preScreening.VaccinationStatus != 1))
            {
                return RedirectToAction("Failure");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: PreScreening/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PreScreening/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PreScreening/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PreScreening/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
