using System.Collections.Generic;
using RecipeeAPI.DTOs.Ingredient;
using RecipeeAPI.DTOs.Method;

namespace RecipeeAPI.DTOs.Recipe
{
    public class GetRecipeShortDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}