using System;

namespace PetMating.Api.Models
{
    public class Address
    {
        public Guid Id { get; set; }

        public string StreetName { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}