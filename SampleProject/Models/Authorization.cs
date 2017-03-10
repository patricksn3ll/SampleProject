using System.ComponentModel.DataAnnotations;

namespace SampleProject.Models
{
    public class Authorization
    {
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsAuthorized { get; set; }
    }
}