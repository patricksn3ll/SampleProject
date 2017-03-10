using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Models
{
    public class Contact
    {
        public Contact()
        {
            Address = new List<Address>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50)]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        [StringLength(50)]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [StringLength(150)]
        [MaxLength(150)]
        [Required]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(200)]
        [MaxLength(200)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Number of Computers")]
        public int NumberOfComputers { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}