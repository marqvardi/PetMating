using System.ComponentModel.DataAnnotations;
using PetMating.Api.Models;

namespace PetMating.Api.DTOs.User
{
    public class UserForRegisterDto
    {
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Password should have minimum of 4 chars and 8 maximum.")]
        public string Password { get; set; }

        public Address Address { get; set; }

        public string ImageUrl { get; set; }
    }
}