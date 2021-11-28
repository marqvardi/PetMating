using System;
using PetMating.Api.DTOs.User;

namespace PetMating.Api.DTOs.Animal
{
    public class ReturnAnimalDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }

        public UserOutputDto User { get; set; }

        public int Eyes { get; set; }

        public int Colour { get; set; }

        public int HairType { get; set; }

        public int AmimalType { get; set; }

        public bool Pedigree { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public int Sex { get; set; }

        public string Image { get; set; }
    }
}