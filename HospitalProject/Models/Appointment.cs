using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalProject.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public DateTime Time { get; set; }

        [Required]
        public string UserId { get; set; }

        public int StaffId { get; set; }

        public Staff Staff { get; set; }
    }
}