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
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    public class PreScreeningsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/PreScreeningsData
        public IQueryable<PreScreening> GetPreScreenings()
        {
            return db.PreScreenings;
        }

        // GET: api/PreScreeningsData/5
        [ResponseType(typeof(PreScreening))]
        public IHttpActionResult GetPreScreening(int id)
        {
            PreScreening preScreening = db.PreScreenings.Find(id);
            if (preScreening == null)
            {
                return NotFound();
            }

            return Ok(preScreening);
        }

        // PUT: api/PreScreeningsData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPreScreening(int id, PreScreening preScreening)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != preScreening.Id)
            {
                return BadRequest();
            }

            db.Entry(preScreening).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreScreeningExists(id))
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

        // POST: api/PreScreeningsData
        [ResponseType(typeof(PreScreening))]
        public IHttpActionResult PostPreScreening(PreScreening preScreening)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PreScreenings.Add(preScreening);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = preScreening.Id }, preScreening);
        }

        // DELETE: api/PreScreeningsData/5
        [ResponseType(typeof(PreScreening))]
        public IHttpActionResult DeletePreScreening(int id)
        {
            PreScreening preScreening = db.PreScreenings.Find(id);
            if (preScreening == null)
            {
                return NotFound();
            }

            db.PreScreenings.Remove(preScreening);
            db.SaveChanges();

            return Ok(preScreening);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PreScreeningExists(int id)
        {
            return db.PreScreenings.Count(e => e.Id == id) > 0;
        }
    }
}