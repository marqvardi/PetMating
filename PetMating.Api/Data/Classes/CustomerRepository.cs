using System.Linq;
using PetMating.Api.Data.Interface;
using PetMating.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PetMating.Api.Data.Classes
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public void Update(Customer customer)
        {
            // var objFromDb = _db.Customer.FirstOrDefault(d => d.Id == customer.Id);
            // _db.Entry(customer).State = EntityState.Modified;
            // _db.SaveChanges();
        }
    }
}