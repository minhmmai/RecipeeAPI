using System.Collections.Generic;
using RecipeeAPI.Models;

namespace RecipeeAPI.DTOs.Recipe
{
    public class GetRecipeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Serves { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Method> Methods { get; set; }
        public List<Review> Reviews { get; set; }
    }
}