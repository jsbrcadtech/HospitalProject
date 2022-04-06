using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // A staff is attached one specialization
        // A specialization can be attached to numerous staff 
        [ForeignKey("Specializations")]
        public int SpecializationId { get; set; }
        public virtual Specialization Specializations { get; set; }
    }

    public class StaffsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }


    }
}