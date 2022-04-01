using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateAnnouncement
    {
        //This viewmodel is created to show the staff list available to the announcement page /Announcement/Update/{id}

        //already existing selected announcement details

        public AnnouncementDto SelectedAnnouncement { get; set; }

        //all staffs to choose from when updating the announcement

        public IEnumerable<StaffsDto> StaffOptions { get; set; }

    }
}