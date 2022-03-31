using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateParkingSpot
    {
        //This viewmodel stores information needed to present to /ParkinSpot/Update/{}

        //the existing Parking Spot information
        public ParkingSpotDto SelectedParkingSpot { get; set; }

        // All parking spots items to choose from when updating a parking spot

        public IEnumerable<StaffsDto> StaffsOptions { get; set; }


    }
}