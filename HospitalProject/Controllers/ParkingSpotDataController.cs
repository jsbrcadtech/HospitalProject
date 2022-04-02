using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalProject.Models;
using System.Diagnostics;


namespace HospitalProject.Controllers
{
    public class ParkingSpotDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// Returns all parking spots in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: All parking spots in the database, including their related staff.
        /// </returns>
        /// <example>
        /// GET: api/ParkingSpotData/ListParkingSpots
        /// </example>
        [ResponseType(typeof(ParkingSpotDto))]
        [HttpGet]
        public IHttpActionResult ListParkingSpots()
        {
            List<ParkingSpot> ParkingSpots = db.ParkingSpots.ToList();
            List<ParkingSpotDto> ParkingSpotDtos = new List<ParkingSpotDto>();

            ParkingSpots.ForEach(p => ParkingSpotDtos.Add(new ParkingSpotDto()
            {
                Id = p.Id,
                Code = p.Code,
                Type = p.Type,
                StaffId = p.Staffs.Id
            }));
            return Ok(ParkingSpotDtos);
        }

        /// <summary>
        /// Gathers information about parking spots associated to a specific staff
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: All Parking Spots in the database that match to a specific Staff
        /// </returns>
        /// <param name="id"> ParkingSpot</param>
        /// <example>
        /// GET: api/ParkingSpotdata/ListParkingSpotsForStaff/6
        /// </example>
        [HttpGet]
        [ResponseType(typeof(ParkingSpotDto))]
        public IEnumerable<ParkingSpotDto> ListParkingSpotsForStaff(int id)
        {
            //all ParkingSpots that have staffs which match with the ID
            List<ParkingSpot> ParkingSpots = db.ParkingSpots.Where(p => p.StaffId == id).ToList();
            List<ParkingSpotDto> ParkingSpotsDtos = new List<ParkingSpotDto>();

            ParkingSpots.ForEach(p => ParkingSpotsDtos.Add(new ParkingSpotDto()
            {
                Id = p.Id,
                Code = p.Code,
                Type = p.Type,
                Name = p.Staffs.Name
            }));
            return ParkingSpotsDtos;
        }

        /// <summary>
        /// Returns all parking spots in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A parking spot in the system matching up to its Id  primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="Id">The primary key of the parking spot</param>
        /// <example>
        /// GET: api/ParkingSpotData/FindParkingSpot/6
        /// </example>
        [ResponseType(typeof(ParkingSpot))]
        [HttpGet]
        public IHttpActionResult FindParkingSpot(int id)
        {
            ParkingSpot ParkingSpot = db.ParkingSpots.Find(id);
            ParkingSpotDto ParkingSpotDto = new ParkingSpotDto()
            {
                Id = ParkingSpot.Id,
                Code = ParkingSpot.Code,
                Type = ParkingSpot.Type,
                StaffId = ParkingSpot.Staffs.Id
            };
            if (ParkingSpot == null)
            {
                return NotFound();
            }

            return Ok(ParkingSpotDto);
        }

        /// <summary>
        /// Updates a parking spot in the system with POST Data input
        /// </summary>
        /// <param name="Id">Represents the parking spot Id primary key</param>
        /// <param name="ParkingSpot">JSON FORM DATA of a ParkingSpot</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/ParkingSpotData/UpdateParkingSpot/6
        /// FORM DATA: ParkingSpot JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateParkingSpot(int id, ParkingSpot ParkingSpot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ParkingSpot.Id)
            {
                return BadRequest();
            }

            db.Entry(ParkingSpot).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingSpotExists(id))
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
        /// Adds a parking spot to the system
        /// </summary>
        /// <param name="ParkingSpot">JSON FORM DATA of a ParkingSpot</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: ParkingSpot Id, ParkingSpot Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/ParkingSpotData/AddParkingSpot
        /// FORM DATA: ParkingSpot JSON Object
        /// </example>
        [ResponseType(typeof(ParkingSpot))]
        [HttpPost]
        public IHttpActionResult AddParkingSpot(ParkingSpot ParkingSpot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParkingSpots.Add(ParkingSpot);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ParkingSpot.Id }, ParkingSpot);
        }


        /// <summary>
        /// Deletes a parking spot from the system by its Id.
        /// </summary>
        /// <param name="Id">The primary key of the ParkingSpot</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/ParkingSpotData/DeleteParkingSpot/6
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(ParkingSpot))]
        [HttpPost]
        public IHttpActionResult DeleteParkingSpot(int id)
        {
            ParkingSpot ParkingSpot = db.ParkingSpots.Find(id);
            if (ParkingSpot == null)
            {
                return NotFound();
            }
            db.ParkingSpots.Remove(ParkingSpot);
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

        private bool ParkingSpotExists(int Id)
        {
            return db.ParkingSpots.Count(e => e.Id == Id) > 0;
        }
    }
}