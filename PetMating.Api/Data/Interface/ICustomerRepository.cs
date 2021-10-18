using PetMating.Api.Data.Classes;
using PetMating.Api.Models;

namespace PetMating.Api.Data.Interface
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void Update(Customer customer);
    }
}