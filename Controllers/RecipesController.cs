using System;
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
    [Authorize]
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

        /// <summary>
        /// Get all recipes created by the logged in user
        /// </summary>
        [HttpGet()]
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
        /// Get a recipe using its Id
        /// </summary>
        /// <param name="recipeId">Id of the recipe to retrieve</param>
        /// <returns></returns>
        [HttpGet("{recipeId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRecipeById(int recipeId)
        {
            ServiceResponse<GetRecipeDTO> response = await _recipeService.GetRecipeById(recipeId);

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
        public async Task<IActionResult> AddRecipe([FromBody] AddRecipeDTO request)
        {
            ServiceResponse<GetRecipeDTO> response = await _recipeService.AddRecipe(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Update a recipe
        /// </summary>
        /// <param name="recipeId">Id of the recipe to be updated</param>
        /// <param name="request">DTO for updating a recipe</param>
        [HttpPut("{recipeId}")]
        public async Task<IActionResult> UpdateRecipe(int recipeId, UpdateRecipeDTO request)
        {
            ServiceResponse<GetRecipeDTO> response = await _recipeService.UpdateRecipe(recipeId, request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        //[AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("success");
        }

    }
}