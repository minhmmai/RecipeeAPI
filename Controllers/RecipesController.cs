using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeeAPI.DTOs.Recipe;
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
        /// <summary>
        /// Constructor that initializes the recipe controller.
        /// </summary>
        public RecipesController()
        {
        }

        /// <summary>
        /// Test authorization
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public string Test()
        {
            return "Success";
        }
    }
}