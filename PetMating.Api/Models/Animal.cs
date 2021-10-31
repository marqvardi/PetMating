using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMating.Api.Models
{
    public class Animal
    {
        public Guid Id { get; set; }

        [Required]
        public Colour Eyes { get; set; }

        [Required]
        public Colour Colour { get; set; }

        [Required]
        public HairType HairType { get; set; }

        [Required]
        public AnimalType AmimalType { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required]
        public bool Pedigree { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Do not enter more than 15 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Do not enter more than 15 characters")]
        public string LastName { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        public string Image { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        [NotMapped]
        public int Age { get { return (DOB.Year - DateTime.Now.Year); } }

    }

    public enum Sex
    {
        Male,
        Female
    }

    public enum Colour
    {
        Brown,
        Black,
        Red,
        White,
        Caramel,
        Grey
    }

    public enum HairType
    {
        Long,
        Short
    }

    public enum AnimalType
    {
        Dog,
        Cat
    }
}