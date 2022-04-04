using HospitalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HospitalProject.Controllers
{
    [RoutePrefix("api/appointments")]
    public class AppointmentDataController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [Route("")]
        [HttpPost]
        public IHttpActionResult AddAppointment(Appointment appointment)
        {
            return null;
        }
    }
}
