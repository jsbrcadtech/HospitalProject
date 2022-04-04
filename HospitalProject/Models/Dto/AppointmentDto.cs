using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Models.Dto
{
    public class AppointmentDto
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
}