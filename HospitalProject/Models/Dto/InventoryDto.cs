using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Models.Dto
{
    public class InventoryDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int BaseQuantity { get; set; }
    }
}
