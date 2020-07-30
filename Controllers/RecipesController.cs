using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeeAPI.DTOs.Recipe;
using RecipeeAPI.Services;
using RecipeeAPI.Services.RecipeService;

namespace RecipeeAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private IRecipeService _rescipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _rescipeService = recipeService;
        }

        [HttpGet]
        [Route("~/recipes")]
        public async Task<IActionResult> GetAllRecipes()
        {
            return Ok(await _rescipeService.GetAllRecipes());
        }        

        [AllowAnonymous]
        [HttpGet]
        [Route("~/recipes/{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            return Ok(await _rescipeService.GetRecipeById(id));
        }

        [HttpPost]
        [Route("~/recipes")]
        public async Task<IActionResult> AddRecipe(AddRecipeDTO newRecipe)
        {
            return Ok(await _rescipeService.AddRecipe(newRecipe));
        }

        [HttpPut]
        [Route("~/recipes/{id}")]
        public async Task<IActionResult> UpdateRecipe(int id,UpdateRecipeDTO updatedRecipe)
        {
            ServiceResponse<GetRecipeDTO> response = new ServiceResponse<GetRecipeDTO>();
            if(updatedRecipe == null)
            {
                return NotFound(response);
            }
            return Ok(await _rescipeService.UpdateRecipe(id, updatedRecipe));
        }
    }
}