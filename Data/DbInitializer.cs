using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.Data
{
    /// <summary>
    /// Class used to seed the database.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Write initial data to the database.
        /// </summary>
        /// <param name="context">The context of the database to be seeded.</param>
        public static void Initialize(RecipeeContext context)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;
            }

            var roles = new IdentityRole[]
            {
                new IdentityRole{Name = "admin", NormalizedName = "ADMIN"},
                new IdentityRole{Name = "moderator", NormalizedName = "MODERATOR"},
                new IdentityRole{Name = "member", NormalizedName = "MEMBER"}
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();
        }
    }
}
