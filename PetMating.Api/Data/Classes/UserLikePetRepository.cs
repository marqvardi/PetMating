using PetMating.Api.Data.Interface;
using PetMating.Api.Models;

namespace PetMating.Api.Data.Classes
{
    public class UserLikePetRepository : Repository<UserLikePet>, IUserLikePetRepository
    {
        private readonly ApplicationDbContext _context;
        public UserLikePetRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;

        }
    }
}