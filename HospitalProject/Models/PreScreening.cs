using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class PreScreening
    {
        [Key]
        public int Id { get; set; }
        public int VaccinationStatus { get; set; }

        // A prescreening is attached to one patient
        // A patient can have many prescreenings
        [ForeignKey("Patients")]
        public int PatientId { get; set; }
        public virtual Patient Patients { get; set; }

    }
}