using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManagementApp.Models;

namespace TeamManagementApp.Data
{

    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<FileOnDatabase> FilesOnDatabase { get; set; }
        public DbSet<KanbanData> KanbanData { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserPreferences> UserPreferences { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

            //many-to-many relationship configuration between users and teams
            builder.Entity<AppUser>()
                .HasMany(u => u.Teams)
                .WithMany(t => t.Members)
                .UsingEntity(j => j.ToTable("UserTeams"));
        }
    }

    internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(50);
            builder.Property(u => u.LastName).HasMaxLength(50);
        }
    }
}
