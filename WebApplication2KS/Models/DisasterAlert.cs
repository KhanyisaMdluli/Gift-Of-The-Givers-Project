using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2KS.Models
{
    public class DisasterAlert
    {
        [Key]
        public int AlertId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }

        public virtual User User { get; set; }
        public virtual ReliefEffort ReliefEffort { get; set; }
    }
}
