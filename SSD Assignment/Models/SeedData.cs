using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace SSD_Assignment.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.ApplicationUsers.Any())
                {
                    return;   // DB has been seeded
                }

                context.ApplicationUsers.AddRange(
                    new ApplicationUser
                    {
                        Id = "3e3c9390 - cf13 - 4d14 - b7a4 - b370c205ca99",
                        AccessFailedCount = 0,
                        BirthDate = new DateTime(2000, 1, 1, 12, 00, 00),
                        ConcurrencyStamp = "4f50e518-41de-46a8-93af-9a13648ace6b",
                        Email = "admin@gmail.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        LockoutEnd = null,
                        Name = "Admin",
                        NormalizedEmail = "ADMIN@GMAIL.COM",
                        NormalizedUserName = "ADMIN@GMAIL.COM",
                        PasswordHash = "AQAAAAEAACcQAAAAEOdrqoYbcS9dS9xX+QmXDwg56p48FJTkrj3CfgU/f8rCf+smteqgJIdQtFQ+tbU+zw==",
                        PhoneNumber = null,
                        PhoneNumberConfirmed = false,
                        ProfilePic = "50888506-9c77-4633-8a7a-1b2eee25233dNP ICT logo.jpg",
                        SecurityStamp = "fae51419-eac4-4ffd-a4a9-800d70409292",
                        TwoFactorEnabled = true,
                        UserName = "admin@gmail.com"
                    }
                );

                context.ApplicationRole.AddRange(
                    new ApplicationRole
                    {
                        Id = "1e2765a7 - 257f - 4602 - b4fc - c7424e5a5b9b",
                        ConcurrencyStamp = "ccfcf113-20fb-4492-97b6-d95bfe270d92",
                        CreatedDate = new DateTime(2018, 7, 27, 19, 32, 35),
                        Description = "Admin role",
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    }
                );

               context.SaveChanges();
            }
        }
    }
}