using System.ComponentModel.DataAnnotations;

namespace NewRegitration.Models
{
    public class UpdateUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }

        [Required]
        public string NewFirstName { get; set; }

        [Required]
        public string NewLastName { get; set; }
    }
}

