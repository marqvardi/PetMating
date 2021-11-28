using System;

namespace PetMating.Api.Models
{
    public class UserLikePet
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public Guid AnimalId { get; set; }

        public Animal Animal { get; set; }
        public bool IsFavourite { get; set; }
    }
}