using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMating.Api.Models
{
    public class Customer
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Do not enter more than 15 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Do not enter more than 15 characters")]
        public string LastName { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        public string Email { get; set; }

        public Guid AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        public ICollection<Animal> Animal { get; set; }


        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        [NotMapped]
        public int Age { get { return (DateTime.Now.Year - DOB.Year); } }
    }
}