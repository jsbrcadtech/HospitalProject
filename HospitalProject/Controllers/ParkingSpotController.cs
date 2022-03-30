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
    public class ParkingSpotController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ParkingSpotController()
        {
            HttpClientHandler handler = new HttpClientHandler()
                {
                    AllowAutoRedirect = false,
                    //cookies are manually set in RequestHeader
                    UseCookies = false
                };
                client = new HttpClient(handler);
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
            Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }

        // GET: ParkingSpot/List
        public ActionResult List()
        {
            //Objective: Communicate with parkingspot data api to retrieve a list of parkingspots
            //curl https://localhost:44397/api/parkingspotdata/listparkingspots

            string url = "parkingspotdata/listparkingspots";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("THE RESPONSE IS");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<ParkingSpotDto> ParkingSpots = response.Content.ReadAsAsync<IEnumerable<ParkingSpotDto>>().Result;
            //Debug.WriteLine("Number of parking Spots received : ");
            //Debug.WriteLine(parkingspots.Count());

            return View(ParkingSpots);
        }

        // GET: ParkingSpot/Details/6
        public ActionResult Details(int id)
        {
            DetailsParkingSpot ViewModel = new DetailsParkingSpot();

            //objective: communicate with Parking Spot Data Api to retrieve Parking Spot
            //curl https://localhost:44397/api/parkingspotdata/findparkingspot/{id}

            string url = "parkingspotdata/findparkingspot/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("THE RESPONSE IS");
            //Debug.WriteLine(response.StatusCode);
           
            ParkingSpotDto SelectedParkingSpot = response.Content.ReadAsAsync<ParkingSpotDto>().Result;
            //Debug.WriteLine("Number of parking spots received");
            //Debug.WriteLine(SelectedParkingSpot.Code);

            ViewModel.SelectedParkingSpot = SelectedParkingSpot;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: ParkingSpot/New
        public ActionResult New()
        {
            //information about all staffs in the system.
            //GET api/staffdata/liststaffs

            string url = "staffdata/liststaffs";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<StaffsDto> StaffOptions = response.Content.ReadAsAsync<IEnumerable<StaffsDto>>().Result;

            return View(StaffOptions);
        }



        //GET: ParkingSpot/Create
        [HttpPost]
        public ActionResult Create(ParkingSpot ParkingSpot)
        {
            GetApplicationCookie();//get ParkingSpot credentials
            //Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(ParkingSpot.ParkingSpot);
            //objective: add a new parking spot into the system using the API
            //curl -H "Content-Type:application/json" -d @parkingspot.json https://localhost:44397/api/parkingspotdata/addparkingspot 
            string url = "parkingspotdata/addparkingspot";

            string jsonpayload = jss.Serialize(ParkingSpot);
            //Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            //content.Headers.ContentType.MediaType = "application/json";

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

        // GET: ParkingSpot/Edit/6
        public ActionResult Edit(int id)
        {
            UpdateParkingSpot ViewModel = new UpdateParkingSpot();

            //the existing ParkingSpot information
            string url = "parkingspotdata/findparkingspot/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ParkingSpotDto SelectedParkingSpot = response.Content.ReadAsAsync<ParkingSpotDto>().Result;
            ViewModel.SelectedParkingSpot = SelectedParkingSpot;

            // all staff to choose from when updating this parkingspot
            //the existing parkingspot information
            url = "staffdata/liststaffs/";
            response = client.GetAsync(url).Result;
            IEnumerable<StaffsDto> StaffsOptions = response.Content.ReadAsAsync<IEnumerable<StaffsDto>>().Result;

            ViewModel.StaffsOptions = StaffsOptions;

            return View(ViewModel);
        }

        // POST: ParkingSpot/Update/6
        [HttpPost]
        public ActionResult Update(int id, ParkingSpot ParkingSpot)
        {
            GetApplicationCookie();//get token credentials
            string url = "parkingspotdata/updateparkingspot/" + id;
            string jsonpayload = jss.Serialize(ParkingSpot);
            HttpContent content = new StringContent(jsonpayload);
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                MultipartFormDataContent requestcontent = new MultipartFormDataContent();
                response = client.PostAsync(url, requestcontent).Result;

                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: ParkingSpot/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "parkingspotdata/findparkingspot/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ParkingSpotDto SelectedParkingSpot = response.Content.ReadAsAsync<ParkingSpotDto>().Result;
            return View(SelectedParkingSpot);
        }

        // POST: ParkingSpot/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie(); //Gets authentication token credentials 
            string url = "parkingspotdata/deleteparkingspot/" + id;
            HttpContent content = new StringContent("");
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
