using System.ComponentModel.DataAnnotations;

namespace WebApplication2KS.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }

        // Navigation properties
        public virtual ICollection<Volunteer> Volunteers { get; set; }
        public virtual ICollection<DisasterAlert> DisasterAlerts { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
    }
}
