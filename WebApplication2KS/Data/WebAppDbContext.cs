using Microsoft.EntityFrameworkCore;

namespace WebApplication2KS.Data
{
    public class WebAppDbContext: DbContext
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
        {
        }
        public DbSet<WebApplication2KS.Models.User> Users { get; set; }
        public DbSet<WebApplication2KS.Models.Volunteer> Volunteers { get; set; }
        public DbSet<WebApplication2KS.Models.Donation> Donations { get; set; }
        public DbSet<WebApplication2KS.Models.ReliefEffort> ReliefEfforts { get; set; }
        public DbSet<WebApplication2KS.Models.EffortAssigned> EffortAssigneds { get; set; }
        public DbSet<WebApplication2KS.Models.DisasterAlert> DisasterAlerts { get; set; }

        
    }
}
