using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AnimalStoreMvc.Models.Domain;

namespace AnimalStoreMvc.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
    }
        public DbSet<Animaltype> Animaltype { get; set; }
        public DbSet<AnimaltypeAnimal> AnimaltypeAnimal { get; set; }
        public DbSet<Animal> Animal { get; set; }
    }
}
