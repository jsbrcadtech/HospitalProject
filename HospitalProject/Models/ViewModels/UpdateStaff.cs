using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateStaff
    {
            //This viewmodel stores information needed to present to /Staff/Update/{}
            //the existing Staff information
            public StaffsDto SelectedStaff { get; set; }

            // All Parking Spots to choose from when updating this Staff
            public IEnumerable<SpecializationsDto> SpecializationsOptions { get; set; }
    }
}