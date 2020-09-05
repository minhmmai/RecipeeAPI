using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.Data
{
    public static class DbInitializer
    {
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
