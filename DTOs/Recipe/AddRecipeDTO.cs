using System.Collections.Generic;
using RecipeeAPI.DTOs.Ingredient;
using RecipeeAPI.DTOs.Method;

namespace RecipeeAPI.DTOs.Recipe
{
    public class AddRecipeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Serves { get; set; }
        public List<GetIngredientDTO> Ingredients { get; set; }
        public List<GetMethodDTO> Methods { get; set; }
    }
}