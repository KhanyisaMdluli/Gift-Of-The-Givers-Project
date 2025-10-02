using System.ComponentModel.DataAnnotations;

namespace WebApplication2KS.Models
{
    public class ReliefEffort
    {
        [Key]
        public int EffortId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<EffortAssigned> EffortAssignments { get; set; }
        public virtual ICollection<DisasterAlert> DisasterAlerts { get; set; }
    }
}
