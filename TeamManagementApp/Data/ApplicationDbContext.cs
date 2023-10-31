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
		public DbSet<UserTeam> UserTeams { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

			// Define primary keys
			builder.Entity<AppUser>().HasKey(q => q.Id);
			builder.Entity<Team>().HasKey(q => q.Id);

			// Define relationships
			builder.Entity<UserTeam>()
				.HasKey(q => new { q.UserId, q.TeamId });

			builder.Entity<UserTeam>()
				.HasOne(userTeam => userTeam.Team)
				.WithMany(team => team.Members)
				.HasForeignKey(userTeam => userTeam.TeamId);

			builder.Entity<UserTeam>()
				.HasOne(userTeam => userTeam.AppUser)
				.WithMany(appUser => appUser.Teams)
				.HasForeignKey(userTeam => userTeam.UserId);




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
