using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class DetailsParkingSpot
    {
        public ParkingSpotDto SelectedParkingSpot { get; set; }

        //Staffs that are assigned to this parking spot
        public IEnumerable<StaffsDto> RelatedStaffs{ get; set; }

    }
}