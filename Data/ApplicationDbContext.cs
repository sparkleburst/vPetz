using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace vPetz.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Database=vPetz;User=root;Password=yourpassword;",
                    new MySqlServerVersion(new Version(8, 0, 30)));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Identity column length for MySQL
            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(m => m.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(m => m.NormalizedName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(m => m.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(m => m.NormalizedUserName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(m => m.NormalizedEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }

        // Define your DbSets (tables) here
        public DbSet<Pet> Pets { get; set; }
        // Add other DbSets as needed
    }
}
