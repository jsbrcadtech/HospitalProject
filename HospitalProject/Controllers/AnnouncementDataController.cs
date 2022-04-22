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
using System.Diagnostics;

namespace HospitalProject.Controllers
{
    public class AnnouncementDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// GET: api/AnnouncementData/ListAnnouncements
        /// api to get all announcements
        /// </summary>
        /// <returns>
        /// OK
        /// {
        /// "Id":12,
        /// "Title:"Announcement for Covid",
        /// "Description":"Only vaccinated people can enter the hospital",
        /// "IsPSA":"True",
        /// "Url":"url"
        /// "Name":"Justus"
        /// }
        /// </returns>

        // GET: api/AnnouncementData/ListAnnouncements
        [HttpGet]
        public IEnumerable<AnnouncementDto> ListAnnouncements()
        {
            List<Announcement> Announcements = db.Announcements.ToList();
            List<AnnouncementDto> AnnouncementDtos = new List<AnnouncementDto>();

            Announcements.ForEach(a => AnnouncementDtos.Add(new AnnouncementDto()
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                IsPSA = a.IsPSA,
                Url = a.Url,
                Name = a.Staffs.Name
            }));
            return AnnouncementDtos;
        }

        /// <summary>
        /// GET: api/AnnouncementData/ListAnnouncementsForStaff/{id}
        /// api to fetch announcement by staffid
        /// </summary>
        /// <param name="id">int id of the announcement to fetch</param>
        /// <returns>
        /// OK
        /// {
        /// "Id":12,
        /// "Title:"Announcement for Covid",
        /// "Description":"Only vaccinated people can enter the hospital",
        /// "IsPSA":"True",
        /// "Url":"url"
        /// "Name":"Justus"
        /// }
        /// NotFound - if announcement with requested id is not in the DB
        /// </returns>

        // GET: api/AnnouncementData/ListAnnouncementsForStaff/5
        [HttpGet]
        public IEnumerable<AnnouncementDto> ListAnnouncementsForStaff(int id)
        {
            List<Announcement> Announcements = db.Announcements.Where(a => a.StaffId == id).ToList();
            List<AnnouncementDto> AnnouncementDtos = new List<AnnouncementDto>();

            Announcements.ForEach(a => AnnouncementDtos.Add(new AnnouncementDto()
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                IsPSA = a.IsPSA,
                Url = a.Url,
                Name = a.Staffs.Name
            }));
            return AnnouncementDtos;
        }


        /// <summary>
        /// GET: api/AnnouncementData/FindAnnouncement/{id}
        /// api to fetch announcement by id
        /// </summary>
        /// <param name="id">int id of the announcement to fetch</param>
        /// <returns>
        /// OK
        /// {
        /// "Id":12,
        /// "Title:"Announcement for Covid",
        /// "Description":"Only vaccinated people can enter the hospital",
        /// "IsPSA":"True",
        /// "Url":"url"
        /// "Name":"Justus"
        /// }
        /// NotFound - if announcement with requested id is not in the DB
        /// </returns>
        // GET: api/AnnouncementData/FindAnnouncement/5
        [ResponseType(typeof(Announcement))]
        [HttpGet]
        public IHttpActionResult FindAnnouncement(int id)
        {
            Announcement Announcement = db.Announcements.Find(id);
            AnnouncementDto AnnouncementDto = new AnnouncementDto()
            {
                Id = Announcement.Id,
                Title = Announcement.Title,
                Description = Announcement.Description,
                IsPSA = Announcement.IsPSA,
                Url = Announcement.Url,
                Name = Announcement.Staffs.Name
            };
            if (Announcement == null)
            {
                return NotFound();
            }

            return Ok(AnnouncementDto);
        }

        /// <summary>
        /// POST: api/AnnouncementData/UpdateAnnouncement/{id}
        /// api to update existing announcemnet
        /// </summary>
        /// <param name="id">int id of the announcemnet to update</param>
        /// <requestBody>
        /// {
        /// "Title:"Announcement for Covid",
        /// "Description":"Only vaccinated people can enter the hospital",
        /// "IsPSA":"True",
        /// "Url":"url"
        /// "Name":"Justus"
        /// }
        /// </requestBody>
        /// <returns>
        /// Ok
        /// {
        /// "Id":12,
        /// "Title:"Announcement for Covid",
        /// "Description":"Only vaccinated people can enter the hospital",
        /// "IsPSA":"True",
        /// "Url":"url"
        /// "Name":"Justus"
        /// }
        /// BadRequest - if request body is invalid
        /// </returns>
        // POST: api/AnnouncementData/UpdateAnnouncement/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAnnouncement(int id, Announcement announcement)
        {
            //Debug.WriteLine("Line1");
            if (!ModelState.IsValid)
            {
                //Debug.WriteLine("invalid");
                return BadRequest(ModelState);
            }

            if (id != announcement.Id)
            {
                //Debug.WriteLine("badrequest");
                return BadRequest();
            }

            db.Entry(announcement).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(id))
                {
                    //Debug.WriteLine("notfound");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //Debug.WriteLine("none is triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// POST: api/AnnouncementData/AddAnnouncement
        /// api to add new announcement
        /// </summary>
        /// <requestBody>
        /// {
        /// "Title:"Announcement for Covid",
        /// "Description":"Only vaccinated people can enter the hospital",
        /// "IsPSA":"True",
        /// "Url":"url"
        /// "Name":"Justus"
        /// }
        /// </requestBody>
        /// <returns>
        /// Ok
        /// {
        /// "Id":12,
        /// "Title:"Announcement for Covid",
        /// "Description":"Only vaccinated people can enter the hospital",
        /// "IsPSA":"True",
        /// "Url":"url"
        /// "Name":"Justus"
        /// }
        /// BadRequest - if request body is invalid
        /// </returns>

        // POST: api/AnnouncementData/AddAnnouncement
        [ResponseType(typeof(Announcement))]
        [HttpPost]
        public IHttpActionResult AddAnnouncement(Announcement announcement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Announcements.Add(announcement);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = announcement.Id }, announcement);
        }

        /// <summary>
        /// DELETE: api/AnnouncementData/DeleteAnnouncement/{id}
        /// api to delete an announcement 
        /// </summary>
        /// <param name="id">int id of the announcemnet to delete</param>
        /// <returns>
        /// Ok
        /// NotFound - if announcemnet with requested id is not in the DB
        /// </returns>
        // POST: api/AnnouncementData/DeleteAnnouncement/5
        [ResponseType(typeof(Announcement))]
        [HttpPost]
        public IHttpActionResult DeleteAnnouncement(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return NotFound();
            }

            db.Announcements.Remove(announcement);
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

        private bool AnnouncementExists(int id)
        {
            return db.Announcements.Count(e => e.Id == id) > 0;
        }
    }
}