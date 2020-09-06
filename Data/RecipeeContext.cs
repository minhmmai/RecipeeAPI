using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.Data
{
    /// <summary>
    /// The Recipee API database context.
    /// </summary>
    public class RecipeeContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Contructor to initialze the context.
        /// </summary>
        /// <param name="options"></param>
        public RecipeeContext(DbContextOptions<RecipeeContext> options) : base(options)
        {
        }

        /// <summary>
        /// The recipes context.
        /// </summary>
        public DbSet<Recipe> Recipes { get; set; }
        /// <summary>
        /// The recipe's ingredient context.
        /// </summary>
        public DbSet<Ingredient> Ingredients { get; set; }
        /// <summary>
        /// The recipe's method context.
        /// </summary>
        public DbSet<Method> Methods { get; set; }
        /// <summary>
        /// The recipe's reviews context.
        /// </summary>
        public DbSet<Review> Reviews { get; set; }
    }
}
