using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2KS.Models
{
    public class Volunteer
    {
        [Key]
        public int VolunteerId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<EffortAssigned> EffortAssignments
        {
            get; set;
        }
    }
}
