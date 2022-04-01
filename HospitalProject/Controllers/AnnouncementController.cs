using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System.Web.Script.Serialization;

namespace HospitalProject.Controllers
{
    public class AnnouncementController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AnnouncementController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/");
        }


        // GET: Announcement/List
        public ActionResult List()
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

        // GET: Announcement/Details/5
        public ActionResult Details(int id)
        {
            //Objective:To retrieve the single announcement based on their Id 
            //curl  https://localhost:44397/api/AnnouncementData/FindAnnouncement/{id}

            
            string url = "AnnouncementData/FindAnnouncement/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine(response.StatusCode);

            AnnouncementDto selectedannouncement = response.Content.ReadAsAsync<AnnouncementDto>().Result;
            //Debug.WriteLine(selectedannouncement.Title);

            return View(selectedannouncement);
        }

        //GET: Error
        public ActionResult Error()
        {
            return View();
        }

        // GET: Announcement/New
        public ActionResult New()
        {
            //information about all staffs in the system
            //Get: api/StaffData/ListStaffs

            string url = "staffdata/liststaffs";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<StaffsDto> StaffsOptions = response.Content.ReadAsAsync<IEnumerable<StaffsDto>>().Result;

            return View(StaffsOptions);
        }

        // POST: Announcement/Create
        [HttpPost]
        public ActionResult Create(Announcement announcement)
        {
            //objective: add a new announcement to our system using the API
            //curl -H "Content-Type:application/json" -d @announcement.json https://localhost:44397/api/AnnouncementData/AddAnnouncement


            string url = "AnnouncementData/AddAnnouncement";

            
            string jsonpayload = jss.Serialize(announcement);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Announcement/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateAnnouncement ViewModel = new UpdateAnnouncement();

            //already existing selected announcement details


            string url = "AnnouncementData/FindAnnouncement/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AnnouncementDto selectedannouncement = response.Content.ReadAsAsync<AnnouncementDto>().Result;
            ViewModel.SelectedAnnouncement = selectedannouncement;

            //all staffs to choose from when updating the announcement

            url = "StaffData/liststaffs";
            response = client.GetAsync(url).Result;
            IEnumerable<StaffsDto> staffoptions = response.Content.ReadAsAsync<IEnumerable<StaffsDto>>().Result;
            ViewModel.StaffOptions = staffoptions;

            return View(ViewModel);
        }

        // POST: Announcement/Update/5
        [HttpPost]
        public ActionResult Update(int id, Announcement announcement)
        {
            string url = "AnnouncementData/UpdateAnnouncement/" + id;
            string jsonpayload = jss.Serialize(announcement);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Announcement/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "AnnouncementData/FindAnnouncement/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AnnouncementDto selectedannouncement = response.Content.ReadAsAsync<AnnouncementDto>().Result;
            return View(selectedannouncement);
        }

        // POST: Announcement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "AnnouncementData/DeleteAnnouncement/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
