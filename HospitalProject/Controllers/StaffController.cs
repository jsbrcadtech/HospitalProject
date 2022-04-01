using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System.Web.Script.Serialization;

namespace HospitalProject.Controllers
{
    public class StaffController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static StaffController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                //cookies are manually set in RequestHeader
                UseCookies = false
            };
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44397/api/");
        }

        /// <summary>
        /// Gets the authentication cookie sent to this controller.
        /// </summary>
        private void GetApplicationCookie()
        {
            string token = "";
            client.DefaultRequestHeaders.Remove("Cookie");
            //if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            //Gets token as it's submitted to the controller
            //Uses it to pass over to the WebAPI.
            //Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }

        // GET: Staff/List
        public ActionResult List()
        {
            //Objective: Communicate with staff data api to retrieve a list of staffs
            //curl https://localhost:44397/api/staffdata/liststaffs

            string url = "staffdata/liststaffs";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("THE RESPONSE IS");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<StaffsDto> Staffs = response.Content.ReadAsAsync<IEnumerable<StaffsDto>>().Result;
            //Debug.WriteLine("Number of staffs received : ");
            //Debug.WriteLine(staffs.Count());

            return View(Staffs);
        }

        // GET: Staffs/Details/3
        public ActionResult Details(int id)
        {
            DetailsStaff ViewModel = new DetailsStaff();

            //objective: communicate with staff data Api to retrieve a staff
            //curl https://localhost:44397/api/staffdata/findstaff/{id}

            string url = "staffdata/findstaff/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("THE RESPONSE IS");
            //Debug.WriteLine(response.StatusCode);

            StaffsDto SelectedStaff = response.Content.ReadAsAsync<StaffsDto>().Result;
            //Debug.WriteLine("Number of staffs received");
            //Debug.WriteLine(SelectedStaff.Name);
            
            ViewModel.SelectedStaff = SelectedStaff;

            return View(ViewModel);

        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Staff/New

        
        public ActionResult New()
        {

            //information about all specializations in the system.
            //GET: api/specialization

            string url = "specialization/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<SpecializationsDto> SpecializationsOptions = response.Content.ReadAsAsync<IEnumerable<SpecializationsDto>>().Result;

            return View(SpecializationsOptions);
        }
        

        //Post: Staff/Create
        [HttpPost]
        public ActionResult Create(Staff Staff)
        {
            GetApplicationCookie();//get user credentials
            //Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(Staff.Staff);
            //objective: add a new Staff into our system using the API
            //curl -H "Content-Type:application/json" -d @Staff.json https://localhost:44397/api/Staffdata/addStaff 
            string url = "staffdata/addstaff";

            string jsonpayload = jss.Serialize(Staff);
            //Debug.WriteLine(jsonpayload);

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

        // GET: Staff/Edit/3
        public ActionResult Edit(int id)
        {
            UpdateStaff ViewModel = new UpdateStaff();

            //the existing Staff information
            string url = "Staffdata/findstaff/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            StaffsDto SelectedStaff = response.Content.ReadAsAsync<StaffsDto>().Result;
            ViewModel.SelectedStaff = SelectedStaff;

            // all specializations to choose from when updating this staff
            //the existing staff information
            url = "specialization/";
            response = client.GetAsync(url).Result;
            IEnumerable<SpecializationsDto> SpecializationsOptions = response.Content.ReadAsAsync<IEnumerable<SpecializationsDto>>().Result;

            ViewModel.SpecializationsOptions = SpecializationsOptions;

            return View(ViewModel);
        }

        // POST: Staff/Update/3
        [HttpPost]
        public ActionResult Update(int id, Staff Staff)
        {
            GetApplicationCookie();//get token credentials
            string url = "staffdata/updatestaff/" + id;
            string jsonpayload = jss.Serialize(Staff);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            //Debug.WriteLine(content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Staff/Delete/3
        public ActionResult DeleteConfirm(int id)
        {
            string url = "staffdata/findstaff/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            StaffsDto SelectedStaffs = response.Content.ReadAsAsync<StaffsDto>().Result;
            return View(SelectedStaffs);
        }

        // POST: Staff/Delete/3
        [HttpPost]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie(); //Gets authentication token credentials 
            string url = "staffdata/deletestaff/" + id;
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