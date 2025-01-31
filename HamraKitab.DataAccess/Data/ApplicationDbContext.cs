using Microsoft.EntityFrameworkCore;
using HamraKitab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamraKitab.DataAccess.Data
{
    public class ApplicationDbContext : DbContext    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>()
                .HasKey(i => i.Id); // Define primary key

            modelBuilder.Entity<Genre>()
                .Property(i => i.Name)
                .IsRequired()// Make sure the Name property is required
                .HasMaxLength(50); // Set a maximum length for Name

            modelBuilder.Entity<Genre>()
                .HasIndex(i => i.Id) // Create an index on Id (if needed)
                .IsUnique(); // Ensure Id is unique if needed
            modelBuilder.Entity<Genre>()
                .HasIndex(i => i.Name) // Create an index on Name (if needed)
                .IsUnique(); // Ensure Name is unique if needed
        }
        public override int SaveChanges()
        {
            var deletedEntities = ChangeTracker.Entries<Genre>()
                .Where(e => e.State == EntityState.Deleted)
                .ToList();

            foreach (var deletedEntity in deletedEntities)
            {
                var deletedId = deletedEntity.Entity.Id;

                // Shift all items with Id greater than the deleted item
                var itemsToUpdate = Genres
                    .Where(i => i.Id > deletedId)
                    .ToList();

                foreach (var item in itemsToUpdate)
                {
                    item.Id -= 1; // Shift ID down by 1
                }
            }

            // Call the base SaveChanges to apply the changes to the database
            return base.SaveChanges();
        }

    }
}
