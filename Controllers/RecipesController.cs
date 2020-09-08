using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeeAPI.DTOs.Recipe;
using RecipeeAPI.Models;
using RecipeeAPI.Services;
using RecipeeAPI.Services.RecipeService;

namespace RecipeeAPI.Controllers
{
    /// <summary>
    /// Controller for all methods associated with a recipe.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        /// <summary>
        /// Constructor that initializes the recipe controller.
        /// </summary>
        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet()]
        [Authorize(Policy = "RegisteredUser")]
        public async Task<IActionResult> GetAllRecipes()
        {
            ServiceResponse<List<GetRecipeDTO>> response = await _recipeService.GetAllRecipes();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Add a recipe
        /// </summary>
        /// <param name="request">DTO for adding a recipe</param>
        /// <returns>Added recipe (GetRecipeDTO)</returns>
        [HttpPost]
        [Authorize(Policy = "RegisteredUser")]
        public async Task<IActionResult> AddRecipe([FromBody] AddRecipeDTO request)
        {
            ServiceResponse<GetRecipeDTO> response = await _recipeService.AddRecipe(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}