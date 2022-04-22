using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalProject.Models;
using System.Diagnostics;

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

        /// <summary>
        /// POST: api/PreScreeningData/AddPreScreening
        /// api to add new prescreening data
        /// </summary>
        /// <requestBody>
        /// {
        /// "CreatedAt:"22-04-2022 03:23:39",
        /// "UserId":"24f69878-ddcf-4d51-af3d-7bd390c15110",
        /// "Vaccinated":"True",
        /// "LastVaccinationDate":"13-04-2022 00:00:00"
        /// "Cough":"False"
        /// "SoarThroat":"False"
        /// "FeverOrChills":"False"
        /// "ShortnessOfBreath":"False"
        /// }
        /// </requestBody>
        /// <returns>
        /// Ok
        /// {
        /// "Id":12,
        /// "CreatedAt:"22-04-2022 03:23:39",
        /// "UserId":"24f69878-ddcf-4d51-af3d-7bd390c15110",
        /// "Vaccinated":"True",
        /// "LastVaccinationDate":"13-04-2022 00:00:00"
        /// "Cough":"False"
        /// "SoarThroat":"False"
        /// "FeverOrChills":"False"
        /// "ShortnessOfBreath":"False"
        /// }
        /// BadRequest - if request body is invalid
        /// </returns>

        // POST: api/PreScreeningData/AddPreScreening
        [Route("")]
        [HttpPost]
        public IHttpActionResult AddPreScreening(PreScreening preScreening)
        {

            Debug.WriteLine("Line1");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("invalid");
                return BadRequest(ModelState);
            }

            RemoveOldData(preScreening.UserId);

            preScreening.CreatedAt = DateTime.Now;
            db.PreScreenings.Add(preScreening);
            db.SaveChanges();
            Debug.WriteLine("none is triggered");
            return CreatedAtRoute("DefaultApi", new { id = preScreening.Id }, preScreening);

        }

        private void RemoveOldData(string userId)
        {
            var preScreening = db.PreScreenings.Where(p => p.UserId == userId);
            db.PreScreenings.RemoveRange(preScreening);
        }
    }
}