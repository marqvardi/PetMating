using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetMating.Api.Data.Interface;
using PetMating.Api.Models;

namespace PetMating.Api.Data.Classes
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        private readonly ApplicationDbContext _db;
        public AnimalRepository(ApplicationDbContext context) : base(context)
        {
            _db = context;
        }

        public Dictionary<int, string> GetAnimalType()
        {
            return Enum.GetValues(typeof(AnimalType)).Cast<AnimalType>().ToDictionary(t => (int)t, t => t.ToString());
        }

        public Dictionary<int, string> GetColours()
        {
            return Enum.GetValues(typeof(Colour)).Cast<Colour>().ToDictionary(t => (int)t, t => t.ToString());
        }

        public Dictionary<int, string> GetHairType()
        {
            return Enum.GetValues(typeof(HairType)).Cast<HairType>().ToDictionary(t => (int)t, t => t.ToString());
        }

        public async void Update(Animal animal)
        {
            var objFromDb = await _db.Animal.FirstOrDefaultAsync(d => d.Id == animal.Id);

            objFromDb.AmimalType = animal.AmimalType;
            objFromDb.Colour = animal.Colour;
            objFromDb.DOB = animal.DOB;
            objFromDb.Eyes = animal.Eyes;
            objFromDb.FirstName = animal.FirstName;
            objFromDb.LastName = animal.LastName;
            objFromDb.HairType = animal.HairType;
            objFromDb.Pedigree = animal.Pedigree;

            if (objFromDb.Image != null)
            {
                objFromDb.Image = animal.Image;
            }
            // _db.Entry(animal).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}