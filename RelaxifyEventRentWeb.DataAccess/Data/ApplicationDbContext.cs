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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Namioty", DisplayOrder = 1},
                new Category { Id = 2, Name = "Podłoga do namiotów", DisplayOrder = 1},
                new Category { Id = 3, Name = "Krzesła", DisplayOrder = 1},
                new Category { Id = 4, Name = "Siedziska", DisplayOrder = 1},
                new Category { Id = 5, Name = "Stoły", DisplayOrder = 1},
                new Category { Id = 6, Name = "Scena", DisplayOrder = 1},
                new Category { Id = 7, Name = "Nagłosnienie", DisplayOrder = 1},
                new Category { Id = 8, Name = "Światło", DisplayOrder = 1},
                new Category { Id = 9, Name = "Kable 230V", DisplayOrder = 1},
                new Category { Id = 10, Name = "Kable siłowe", DisplayOrder = 1},
                new Category { Id = 11, Name = "Roździelnie", DisplayOrder = 1},
                new Category { Id = 12, Name = "Nagrzewnice", DisplayOrder = 1},
                new Category { Id = 13, Name = "Meble z palet", DisplayOrder = 1}
                );
        }
    }
}