using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System.Web.Script.Serialization;
using System.Net.Http;

namespace HospitalProject.Controllers
{
    public class HomeController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/");
        }

        public ActionResult Index()
        {

            //Objective:To retrieve the list of announcements 
            //curl  https://localhost:44397/api/AnnouncementData/ListAnnouncements


            string url = "AnnouncementData/ListAnnouncements";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine(response.StatusCode);

            IEnumerable<AnnouncementDto> announcements = response.Content.ReadAsAsync<IEnumerable<AnnouncementDto>>().Result;
            //Debug.WriteLine(announcements.Count());


            return View(announcements);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}