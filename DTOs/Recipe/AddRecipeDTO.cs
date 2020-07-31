using System.Collections.Generic;
using RecipeeAPI.DTOs.Method;
using RecipeeAPI.DTOs.Ingredient;

namespace RecipeeAPI.DTOs.Recipe
{
    public class AddRecipeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Serves { get; set; }
        public List<AddIngredientDTO> Ingredients { get; set; }
        public List<AddMethodDTO> Methods { get; set; }
    }
}