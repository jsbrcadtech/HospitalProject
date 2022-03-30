using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class DetailsStaff
    {
        public StaffsDto SelectedStaff { get; set; }
        //Parking spots that are assigned to this Staff  
        public IEnumerable<ParkingSpotDto> RelatedParkingSpots { get; set; }
    }
}