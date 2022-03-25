using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace HospitalProject.Models
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPSA { get; set; }
        public string Url { get; set; }

        // An announcement is attached to one staff
        // A staff can be attached to many announcements  
        [ForeignKey("Staffs")]
        public int StaffId { get; set; }
        public virtual Staff Staffs { get; set; }
    }
}