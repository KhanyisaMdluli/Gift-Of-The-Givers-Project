using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2KS.Models
{
    public class EffortAssigned
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Volunteer")]
        public int VolunteerId { get; set; }

        [ForeignKey("ReliefEffort")]
        public int EffortId { get; set; }

        public virtual Volunteer Volunteer { get; set; }
        public virtual ReliefEffort ReliefEffort { get; set; }
    }
}
