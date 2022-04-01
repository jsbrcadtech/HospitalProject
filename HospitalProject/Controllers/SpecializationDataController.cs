using HospitalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HospitalProject.Controllers
{
    [RoutePrefix("api/specialization")]

    public class SpecializationDataController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>
        /// Returns all specializations in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)


        [Route("")]
        [HttpGet]
        public IEnumerable<Specialization> GetAll()
        {
            return _db.Specializations;
        }

        /// <summary>
        /// Returns specialization by its primary ket
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

    }

}