using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Models
{
    public class PreScreening
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool Vaccinated { get; set; }

        public DateTime LastVaccinationDate { get; set; }

        [Required]
        public bool Cough { get; set; }

        [Required]
        public bool SoreThroat { get; set; }

        [Required]
        public bool FeverOrChills { get; set; }

        [Required]
        public bool ShortnessOfBreath { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [MaxLength(128)]
        public string UserId { get; set; }

        public bool IsValid()
        {
            if(Cough|| SoreThroat || FeverOrChills || ShortnessOfBreath || !Vaccinated)
            {
                return false;
            }
            if(LastVaccinationDate > DateTime.Now.AddDays(-14))
            {
                return false;
            }
            if(CreatedAt < DateTime.Now.Date)
            {
                return false;
            }
            return true;
        }
    }
}
