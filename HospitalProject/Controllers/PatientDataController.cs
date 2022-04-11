using HospitalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Diagnostics;


namespace HospitalProject.Controllers
{
    [RoutePrefix("api/patient")]

    public class PatientDataController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>
        /// Adds a patient to the system
        /// </summary>
        /// <param name="patient">JSON FORM DATA of a Patient</param>
        /// <returns>
        /// HEADER: 201 (Created)
        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Patients.Add(patient);
            _db.SaveChanges();
            return Created($"/api/patient/{patient.Id}", patient);
        }

        /// <summary>
        /// Returns all patients in the system
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        [ResponseType(typeof(PatientDto))]
        [HttpGet]

        public IHttpActionResult ListPatients()

        {
            List<Patient> patients = _db.Patients.Include(p => p.Name).ToList();
            List<PatientDto> patientDtos = new List<PatientDto>();

            patients.ForEach(p => patientDtos.Add(new PatientDto()
            {
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                DateOfBirth = p.DateOfBirth,
                Id = p.Id
            }));
            return Ok(patientDtos);
        }

        /// <summary>
        /// Returns patient by primary key
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var patient = _db.Patients.SingleOrDefault(i => i.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        /// <summary>
        /// Deletes a patient from the system by primary key.
        /// </summary>
        /// <param name="Id">The primary key of the ParkingSpot</param>
        /// <returns>
        /// HEADER: 200 (OK)
        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeletePatient([FromUri] int id)
        {
            var patientInDb = _db.Patients.SingleOrDefault(i => i.Id == id);
            if (patientInDb == null)
            {
                return NotFound();
            }

            _db.Patients.Remove(patientInDb);
            _db.SaveChanges();
            return Ok();
        }


        /// <summary>
        /// Updates a patient in the system
        /// </summary>
        /// <param name="Id">Represents the patient id with the primary key</param>
        /// <param name="Patient">JSON FORM DATA of a ParkingSpot</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdatePatient(int id, Patient patient)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.Id)
            {
                return BadRequest();
            }

            _db.Entry(patient).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}
