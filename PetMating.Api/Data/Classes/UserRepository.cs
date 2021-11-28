using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PetMating.Api.Data.Interface;
using PetMating.Api.Models;

namespace PetMating.Api.Data.Classes
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }


    }
}