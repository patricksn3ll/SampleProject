using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Models
{
    public class Summary
    {
        [Display(Name = "Total Computers")]
        public int TotalNumberOfComputers { get; set; }

        [Display(Name = "Total Contacts")]
        public int TotalNumberOfContacts { get; set; }

        [Display(Name = "Home Addresses")]
        public int UsersWithHomeAddresses { get; set; }

        [Display(Name = "Work Addresses")]
        public int UsersWithWorkAddresses { get; set; }
    }
}