using System.Collections.Generic;
using RecipeeAPI.Models;

namespace RecipeeAPI.DTOs.Recipe
{
    public class AddRecipeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Serves { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Method> Methods { get; set; }
    }
}