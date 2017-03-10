using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Models
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }

        [Display(Name = "Address Line One")]
        [StringLength(50)]
        [MaxLength(50)]
        [Required]
        public string AddressLineOne { get; set; }

        [Display(Name = "Address Line Two")]
        [StringLength(50)]
        [MaxLength(50)]
        public string AddressLineTwo { get; set; }

        [Display(Name = "City")]
        [StringLength(150)]
        [MaxLength(150)]
        [Required]
        public string City { get; set; }

        [Display(Name = "State")]
        [StringLength(2)]
        [MaxLength(2)]
        [Required]
        public string State { get; set; }

        [Display(Name = "Type")]
        [StringLength(2)]
        [MaxLength(2)]
        [Required]
        public string Type { get; set; }


        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip")]
        [Range(10000, 99999, ErrorMessage ="Zip must be between 10000 and 99999")]
        public int? Zip { get; set; }
    }
}