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
    [RoutePrefix("api/specialization")]

    public class SpecializationDataController : ApiController
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>
        /// Returns all specializations in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        [ResponseType(typeof(SpecializationsDto))]
        [HttpGet]
        public IHttpActionResult ListSpecializations()
        {
            List<Specialization> specializations = _db.Specializations.Include(s => s.Name).ToList();
            List<SpecializationsDto> specializationsDtos = new List<SpecializationsDto>();

            specializations.ForEach(s => specializationsDtos.Add(new SpecializationsDto()
            {
                Name = s.Name,
                Id = s.Id
            }));

            return Ok(specializationsDtos);
        }

        /// <summary>
        /// Returns specialization by its primary key
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var specialization = _db.Specializations.SingleOrDefault(i => i.Id == id);
            if (specialization == null)
            {
                return NotFound();
            }
            return Ok(specialization);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(Specialization specialization)
        {
            _db.Specializations.Add(specialization);
            _db.SaveChanges();
            return Created($"/api/specialization/{specialization.Id}", specialization);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteSpecialization([FromUri] int id)
        {
            var specializationInDb = _db.Specializations.SingleOrDefault(i => i.Id == id);
            if (specializationInDb == null)
            {
                return NotFound();
            }

            _db.Specializations.Remove(specializationInDb);
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
        public IHttpActionResult UpdateSpecialization(int id, Specialization specialization)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != specialization.Id)
            {
                return BadRequest();
            }

            _db.Entry(specialization).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return StatusCode(System.Net.HttpStatusCode.NoContent);

        }
        /*
        [HttpGet]
        [Route("api/specializationdata/search/{searchKey?}")]
        public IEnumerable<Specialization> Search(string searchKey = null)
        {
            if (searchKey == null)
            {
                return _db.Specializations;
            }
            else
            {
                return _db.Specializations.Where(s => s.Name.Contains(searchKey));
            }
        }
        */

        [Route("")]
        [ResponseType(typeof(SpecializationsDto))]
        [HttpGet]
        public IHttpActionResult Search(string searchKey = null)
        {
            var specialization = _db.Specializations.AsQueryable();

            if (searchKey != null)
            {
                specialization = specialization.Where(s => s.Name.Contains(searchKey));
            }

            return Ok(specialization);

        }








    }

}
