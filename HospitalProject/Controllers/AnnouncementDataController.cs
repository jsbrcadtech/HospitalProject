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

        //"AnnouncementData/FindAnnouncementsForStaff/" + id

        // GET: api/AnnouncementData/ListAnnouncementsForStaff/5
        [HttpGet]
        public IEnumerable<AnnouncementDto> ListAnnouncementsForStaff(int id)
        {
            List<Announcement> Announcements = db.Announcements.Where(a=>a.StaffId==id).ToList();
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