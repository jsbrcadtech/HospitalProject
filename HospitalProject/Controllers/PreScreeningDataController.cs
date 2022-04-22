using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    [RoutePrefix("api/prescreening")]
    public class PreScreeningDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("{userId}")]
        [HttpGet]
        public IHttpActionResult GetPreScreeningByUserId(string userId)
        {
            var preScreening = db.PreScreenings.SingleOrDefault(p => p.UserId == userId);

            if (preScreening == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(preScreening);
            }
        }

        // POST: api/PreScreeningData/AddPreScreening
        [Route("")]
        [HttpPost]
        public IHttpActionResult AddPreScreening(PreScreening preScreening)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RemoveOldData(preScreening.UserId);

            preScreening.CreatedAt = DateTime.Now;
            db.PreScreenings.Add(preScreening);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = preScreening.Id }, preScreening);
        }

        private void RemoveOldData(string userId)
        {
            var preScreening = db.PreScreenings.Where(p => p.UserId == userId);
            db.PreScreenings.RemoveRange(preScreening);
        }
    }
}