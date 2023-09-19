using Microsoft.AspNetCore.Identity;
using TeamManagementApp.Models;

namespace TeamManagementApp.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext _context, ILogger<ApplicationDbContextSeed> logger)
        {
            if (_context.KanbanData.Count() == 0)
            {
                _context.KanbanData.AddRange(GetPreconfiguredKanbanData());
                await _context.SaveChangesAsync();
                logger.LogInformation("Seed with Kanban data to database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

            if (_context.Users.Count() == 0)
            {
                _context.Users.AddRange(GetPreconfiguredUsers());
                await _context.SaveChangesAsync();
                logger.LogInformation("Seed with Users data to database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

            if (_context.Roles.Count() == 0)
            {
                _context.Roles.AddRange(GetPreconfiguredRoles());
                await _context.SaveChangesAsync();
                logger.LogInformation("Seed with Roles data to database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

            if (_context.UserRoles.Count() == 0)
            {
                _context.UserRoles.AddRange(GetPreconfiguredUserRoles());
                await _context.SaveChangesAsync();
                logger.LogInformation("Seed with UserRoles data to database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }
        }

        private static IEnumerable<KanbanData> GetPreconfiguredKanbanData()
        {
            return new List<KanbanData>
            {
                new KanbanData() {Summary = "Refactor Basket.API Code", RankId = 5, AssigneeId = "04ab739e-ab75-4e1f-bb0e-c2c77d6b6a28", Assignee = "Stefan Kolaric", AssignedById = "abd3c525-3935-4527-9973-49a8279f9234", AssignedBy = "Petar Petrovic", Priority = "Medium", Status = "Review" },
                new KanbanData() {Summary = "Design UI for the store", RankId = 2, AssigneeId = "abd3c525-3935-4527-9973-49a8279f9234", Assignee = "Petar Petrovic", AssignedById = "abd3c525-3935-4527-9973-49a8279f9234", AssignedBy = "Petar Petrovic", Priority = "Low", Status = "Open" },
                new KanbanData() {Summary = "Add the filtering functionality", RankId = 3, AssigneeId = "abd3c525-3935-4527-9973-49a8279f9234", Assignee = "Petar Petrovic", AssignedById = "abd3c525-3935-4527-9973-49a8279f9234", AssignedBy = "Petar Petrovic", Priority = "Medium", Status = "Open" },
                new KanbanData() {Summary = "Configure Database for the Basket.API", RankId = 4, AssigneeId = "04ab739e-ab75-4e1f-bb0e-c2c77d6b6a28", Assignee = "Stefan Kolaric", AssignedById = "abd3c525-3935-4527-9973-49a8279f9234", AssignedBy = "Petar Petrovic", Priority = "Medium", Status = "Open" },
                new KanbanData() {Summary = "Write Basket.API Code", RankId = 1, AssigneeId = "04ab739e-ab75-4e1f-bb0e-c2c77d6b6a28", Assignee = "Stefan Kolaric", AssignedById = "abd3c525-3935-4527-9973-49a8279f9234", AssignedBy = "Petar Petrovic", Priority = "High", Status = "Close" },
                new KanbanData() {Summary = "Modify OcelotAPI endpoints", RankId = 6, AssigneeId = "b2d2385b-74f1-496c-b808-2cc65a77f2bd", Assignee = "Ivan Ivanovic", AssignedById = "abd3c525-3935-4527-9973-49a8279f9234", AssignedBy = "Petar Petrovic", Priority = "Critical", Status = "Close" },
                new KanbanData() {Summary = "Implement elastic search", RankId = 7, AssigneeId = "98d72d46-0519-4e7a-8029-eb8ff22e96cc", Assignee = "Nenad Nenadovic", AssignedById = "abd3c525-3935-4527-9973-49a8279f9234", AssignedBy = "Petar Petrovic", Priority = "Low", Status = "Close" },
                new KanbanData() {Summary = "Configure Redis Cache for the Basket.API", RankId = 8, AssigneeId = "04ab739e-ab75-4e1f-bb0e-c2c77d6b6a28", Assignee = "Stefan Kolaric", AssignedById = "abd3c525-3935-4527-9973-49a8279f9234", AssignedBy = "Petar Petrovic", Priority = "Medium", Status = "InProgress" },
                new KanbanData() {Summary = "Write HomeController code for CRUD operations", RankId = 9, AssigneeId = "98d72d46-0519-4e7a-8029-eb8ff22e96cc", Assignee = "Nenad Nenadovic", AssignedById = "abd3c525-3935-4527-9973-49a8279f9234", AssignedBy = "Petar Petrovic", Priority = "High", Status = "InProgress" },
                new KanbanData() {Summary = "Scaffold Identity for Google Auth", RankId = 10, AssigneeId = "98d72d46-0519-4e7a-8029-eb8ff22e96cc", Assignee = "Nenad Nenadovic", AssignedById = "abd3c525-3935-4527-9973-49a8279f9234", AssignedBy = "Petar Petrovic", Priority = "Medium", Status = "InProgress" },
                new KanbanData() {Summary = "Modify onCreate function so it increments the RankId by one", RankId = 11, AssigneeId = "98d72d46-0519-4e7a-8029-eb8ff22e96cc", Assignee = "Nenad Nenadovic", AssignedById = "abd3c525-3935-4527-9973-49a8279f9234", AssignedBy = "Petar Petrovic", Priority = "Medium", Status = "Review" },
                new KanbanData() {Summary = "Attend a meeting with the client", RankId = 12, AssigneeId = "b2d2385b-74f1-496c-b808-2cc65a77f2bd", Assignee = "Ivan Ivanovic", AssignedById = "abd3c525-3935-4527-9973-49a8279f9234", AssignedBy = "Petar Petrovic", Priority = "Critical", Status = "Review" },
            };
        }

        private static IEnumerable<AppUser> GetPreconfiguredUsers()
        {
            var hasher = new PasswordHasher<AppUser>();
            return new List<AppUser>
            {
                new AppUser()
                {
                    Id = "04ab739e-ab75-4e1f-bb0e-c2c77d6b6a28", // primary key
                    FirstName = "Stefan",
                    LastName = "Stefanovic",
                    UserName = "stefan@gmail.com",
                    NormalizedUserName = "STEFAN@GMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Xo123456789."),
                    Email = "stefan@gmail.com",
                    NormalizedEmail = "STEFAN@GMAIL.COM"
                },
                new AppUser()
                {
                    Id = "abd3c525-3935-4527-9973-49a8279f9234", // primary key
                    FirstName = "Petar",
                    LastName = "Petrovic",
                    UserName = "petar@gmail.com",
                    NormalizedUserName = "PETAR@GMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Xo123456789."),
                    Email = "petar@gmail.com",
                    NormalizedEmail = "PETAR@GMAIL.COM"
                },
                new AppUser()
                {
                    Id = "b2d2385b-74f1-496c-b808-2cc65a77f2bd", // primary key
                    FirstName = "Ivan",
                    LastName = "Ivanovic",
                    UserName = "ivan@gmail.com",
                    NormalizedUserName = "IVAN@GMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Xo123456789."),
                    Email = "ivan@gmail.com",
                    NormalizedEmail = "IVAN@GMAIL.COM"
                },
                new AppUser()
                {
                    Id = "98d72d46-0519-4e7a-8029-eb8ff22e96cc", // primary key
                    FirstName = "Nenad",
                    LastName = "Nenadovic",
                    UserName = "nenad@gmail.com",
                    NormalizedUserName = "NENAD@GMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Xo123456789."),
                    Email = "nenad@gmail.com",
                    NormalizedEmail = "NENAD@GMAIL.COM"
                }
            };
        }

        private static IEnumerable<IdentityRole> GetPreconfiguredRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole()
                {
                    Id = "4f823f4e-1c26-11ee-be56-0242ac120002",
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                new IdentityRole()
                {
                    Id = "5b57be66-1c26-11ee-be56-0242ac120002",
                    Name = "Engineer",
                    NormalizedName = "ENGINEER"
                }

            };
        }

        private static IEnumerable<IdentityUserRole<string>> GetPreconfiguredUserRoles()
        {
            return new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>()
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "04ab739e-ab75-4e1f-bb0e-c2c77d6b6a28"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "4f823f4e-1c26-11ee-be56-0242ac120002",
                    UserId = "abd3c525-3935-4527-9973-49a8279f9234"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "5b57be66-1c26-11ee-be56-0242ac120002",
                    UserId = "b2d2385b-74f1-496c-b808-2cc65a77f2bd"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "5b57be66-1c26-11ee-be56-0242ac120002",
                    UserId = "98d72d46-0519-4e7a-8029-eb8ff22e96cc"
                }

            };
        }
    }
}
