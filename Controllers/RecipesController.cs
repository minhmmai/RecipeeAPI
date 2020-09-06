using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeeAPI.DTOs.Recipe;
using RecipeeAPI.Services;
using RecipeeAPI.Services.RecipeService;

namespace RecipeeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        public RecipesController()
        {
        }

        [HttpGet]
        [Authorize]
        public string Test()
        {
            return "Success";
        }
    }
}