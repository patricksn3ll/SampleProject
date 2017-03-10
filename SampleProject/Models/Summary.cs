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

        public List<Contact> Users { get; set; }
    }
}