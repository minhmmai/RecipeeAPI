using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeeAPI.DTOs.Recipe;

namespace RecipeeAPI.Services.RecipeService
{
    public interface IRecipeService
    {
         Task<ServiceResponse<List<GetRecipeDTO>>> GetAllRecipes();
         Task<ServiceResponse<GetRecipeDTO>> GetRecipeById(int id);
         Task<ServiceResponse<GetRecipeDTO>> AddRecipe(AddRecipeDTO newRecipe);
    }
}