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
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _rescipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _rescipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            return Ok(await _rescipeService.GetAllRecipes());
        }        

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            return Ok(await _rescipeService.GetRecipeById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(AddRecipeDTO newRecipe)
        {
            return Ok(await _rescipeService.AddRecipe(newRecipe));
        }

        [HttpPut("{id}")]
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