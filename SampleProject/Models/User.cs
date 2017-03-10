using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Models
{
    public class User
    {
        public User()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

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

    }
}