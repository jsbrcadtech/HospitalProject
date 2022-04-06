using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace HospitalProject.Models
{
    public class ParkingSpot
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }

        // A parking spot is attached to one staff
        // A staff can be attached to many parking spots  
        [ForeignKey("Staffs")]
        public int StaffId { get; set; }
        public virtual Staff Staffs { get; set; }
    }

    public class ParkingSpotDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }

    }

}