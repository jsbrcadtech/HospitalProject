using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // Add a prescreening entity to the system 
        public DbSet<PreScreening> PreScreenings { get; set; }

        // Add a patient entity to the system 
        public DbSet<Patient> Patients { get; set; }

        // Add an appoinment entity to the system 
        public DbSet<Appointment> Appointments { get; set; }

        // Add a staff entity to the system 
        public DbSet<Staff> Staffs { get; set; }

        // Add a specialization entity to the system 
        public DbSet<Specialization> Specializations { get; set; }

        // Add an announcement entity to the system 
        public DbSet<Announcement> Announcements { get; set; }

        // Add a parking spot entity to the system 
        public DbSet<ParkingSpot> ParkingSpots { get; set; }

        // Add an inventory entity to the system 
        public DbSet<Inventory> Inventories { get; set; }

        // Add an inventory ledger entity to the system 
        public DbSet<InventoryLedger> InventoryLedgers { get; set; }




        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}