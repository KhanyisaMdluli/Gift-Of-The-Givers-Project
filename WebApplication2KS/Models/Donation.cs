using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2KS.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
    }
}
