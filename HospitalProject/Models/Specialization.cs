using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Models
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SpecializationsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
