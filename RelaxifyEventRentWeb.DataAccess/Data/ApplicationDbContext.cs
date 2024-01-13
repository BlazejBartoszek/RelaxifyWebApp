using Microsoft.EntityFrameworkCore;
using RelaxifyEventRentWeb.Models;

namespace RelaxifyEventRentWeb.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Namioty", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Podłoga do namiotów", DisplayOrder = 1 },
                new Category { Id = 3, Name = "Krzesła", DisplayOrder = 1 },
                new Category { Id = 4, Name = "Siedziska", DisplayOrder = 1 },
                new Category { Id = 5, Name = "Stoły", DisplayOrder = 1 },
                new Category { Id = 6, Name = "Scena", DisplayOrder = 1 },
                new Category { Id = 7, Name = "Nagłosnienie", DisplayOrder = 1 },
                new Category { Id = 8, Name = "Światło", DisplayOrder = 1 },
                new Category { Id = 9, Name = "Kable 230V", DisplayOrder = 1 },
                new Category { Id = 10, Name = "Kable siłowe", DisplayOrder = 1 },
                new Category { Id = 11, Name = "Roździelnie", DisplayOrder = 1 },
                new Category { Id = 12, Name = "Nagrzewnice", DisplayOrder = 1 },
                new Category { Id = 13, Name = "Meble z palet", DisplayOrder = 1 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    ProductName = "Namiot 3x6",
                    Description = "Czarny namiot o ymiarach 3x6 idealny na wydarzenia plenerowe.",
                    Price = 450,
                    CategoryId = 12,
                    ImageUrl = ""
                },
                 new Product
                 {
                     Id = 2,
                     ProductName = "Namiot 2x2",
                     Description = "Czarny namiot o ymiarach 2x2 idealny na wydarzenia plenerowe.",
                     Price = 250,
                     CategoryId = 11,
                     ImageUrl = ""
                 },
                  new Product
                  {
                      Id = 3,
                      ProductName = "Podesty sceniczne",
                      Description = "Podesty sceniczne allustage idealne na Twoje wydarzenia",
                      Price = 30,
                      CategoryId = 10,
                      ImageUrl=""
                  },
                   new Product
                   {
                       Id = 4,
                       ProductName = "Namiot gwiazda",
                       Description = "Namiot w kształcie gwiazdy, wyglada swietnie na każdym uroczystm wydarzeniu, ale nie tylko",
                       Price = 2760,
                       CategoryId = 1,
                       ImageUrl = ""
                   }                    
                );
        }
    }
}