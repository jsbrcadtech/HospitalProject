using HospitalProject.Models;
using HospitalProject.Models.Dto;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System;

namespace HospitalProject.Controllers
{
    [RoutePrefix("api/appointments")]
    public class AppointmentDataController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [Route("")]
        [HttpPost]
        public IHttpActionResult AddAppointment(AppointmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment = new Appointment
            {
                StaffId = dto.StaffId,
                UserId = User.Identity.GetUserId(),
                Time = dto.Date.Add(dto.Time.TimeOfDay),
            };

            if (appointment.Time < DateTime.Now)
            {
                return BadRequest("Appointment time cannot be less than now");
            }

            _db.Appointments.Add(appointment);
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("{userId}")]
        public IHttpActionResult GetAll(string userId)
        {
            var appointmentsForUser = _db.Appointments
                .Where(u => u.UserId == userId);

            return Ok(appointmentsForUser);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteAppointment(int id)
        {
            var appointmentInDb = _db.Appointments.SingleOrDefault(a => a.Id == id);
            _db.Appointments.Remove(appointmentInDb);
            _db.SaveChanges();
            return Ok();
        }

    }
}
