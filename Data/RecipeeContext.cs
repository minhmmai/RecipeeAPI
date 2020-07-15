using Microsoft.EntityFrameworkCore;
using RecipeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.Data
{
    public class RecipeeContext : DbContext
    {
        public RecipeeContext(DbContextOptions<RecipeeContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Method> Methods { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
