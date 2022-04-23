using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalProject.Models;
using System.Diagnostics;


namespace HospitalProject.Controllers
{
    public class StaffDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Staff in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Staff in the database
        /// </returns>
        /// <example>
        /// GET: api/StaffData/ListStaffs
        /// </example>
        [ResponseType(typeof(StaffsDto))]
        [HttpGet]
        public IHttpActionResult ListStaffs()
        {
            List<Staff> Staffs = db.Staffs.Include(S => S.Specializations).ToList();
            List<StaffsDto> StaffsDtos = new List<StaffsDto>();

            Staffs.ForEach(s => StaffsDtos.Add(new StaffsDto()
            {
                Id = s.Id,
                Name = s.Name,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                SpecializationId = s.SpecializationId,
                Specialization = s.Specializations
            }));
            return Ok(StaffsDtos);
        }

        /// <summary>
        /// Returns all Staff in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A Staff in the system matching up to the its Id primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Staff</param>
        /// <example>
        /// GET: api/StaffData/FindStaff/2
        /// </example>
        [ResponseType(typeof(Staff))]
        [HttpGet]
        public IHttpActionResult FindStaff(int id)
        {
            Staff Staffs = db.Staffs.Find(id);
            Specialization Specialization = db.Specializations.Find(Staffs.SpecializationId);
            StaffsDto StaffsDto = new StaffsDto()
            {
                Id = Staffs.Id,
                Name = Staffs.Name,
                StartTime = Staffs.StartTime,
                EndTime = Staffs.EndTime,
                SpecializationId = Staffs.SpecializationId,
                Specialization = Specialization
            };
            if (Staffs == null)
            {
                return NotFound();
            }

            return Ok(StaffsDto);
        }

        /// <summary>
        /// Updates a Staff in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Staff Id primary key</param>
        /// <param name="Staffs">JSON FORM DATA of a Staff</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/StaffsData/UpdateStaffs/2
        /// FORM DATA: Staffs JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateStaff(int id, Staff Staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Staff.Id)
            {

                return BadRequest();
            }

            db.Entry(Staff).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds a Staff to the system
        /// </summary>
        /// <param name="Staff">JSON FORM DATA of a Staff</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Staff ID, Staffs Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/StaffsData/AddStaff
        /// FORM DATA: Staff JSON Object
        /// </example>
        [ResponseType(typeof(Staff))]
        [HttpPost]
        public IHttpActionResult AddStaff(Staff Staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Staffs.Add(Staff);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Staff.Id }, Staff);
        }

        /// <summary>
        /// Deletes a Staff from the system by its Id.
        /// </summary>
        /// <param name="id">The primary key of the Staff</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/StaffData/DeleteStaff/2
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Staff))]
        [HttpPost]
        public IHttpActionResult DeleteStaff(int id)
        {
            Staff Staff = db.Staffs.Find(id);
            if (Staff == null)
            {
                return NotFound();
            }

            db.Staffs.Remove(Staff);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StaffExists(int id)
        {
            return db.Staffs.Count(e => e.Id == id) > 0;
        }
    }
}