using System.Collections.Generic;

namespace HospitalProject.Models.Dto
{
    public class PagedDto
    {
        public List<Inventory> Inventories { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
