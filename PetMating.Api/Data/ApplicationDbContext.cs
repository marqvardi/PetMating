using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetMating.Api.Models;

namespace PetMating.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLikePet>().HasKey(ua => new { ua.AnimalId, ua.UserId });
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Animal> Animal { get; set; }
        public DbSet<UserLikePet> UserLikePet { get; set; }
    }
}