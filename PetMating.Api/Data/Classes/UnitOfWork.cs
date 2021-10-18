using System.Threading.Tasks;
using PetMating.Api.Data.Interface;

namespace PetMating.Api.Data.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Customer = new CustomerRepository(db);
            Animal = new AnimalRepository(db);
            User = new UserRepository(db);
        }

        public ICustomerRepository Customer { get; private set; }

        public IAnimalRepository Animal { get; private set; }
        public IUserRepository User { get; private set; }

        public async Task<int> Complete()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}