using System.Collections.Generic;
using PetMating.Api.Models;

namespace PetMating.Api.Data.Interface
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        Dictionary<int, string> GetColours();
        Dictionary<int, string> GetHairType();
        Dictionary<int, string> GetAnimalType();

        void Update(Animal animal);
    }
}