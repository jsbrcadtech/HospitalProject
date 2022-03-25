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
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Time { get; set; }

        // An appointment is attached to one patient
        // A patient can have many appointments 
        [ForeignKey("Patients")]
        public int PatientId { get; set; }
        public virtual Patient Patients { get; set; }

        // An appointment is attached to one staff
        // A staff can have many appointments 
        [ForeignKey("Staffs")]
        public int StaffId { get; set; }
        public virtual Staff Staffs { get; set; }

    }
}