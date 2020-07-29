using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RecipeeAPI.Data;
using RecipeeAPI.DTOs.Ingredient;
using RecipeeAPI.DTOs.Recipe;
using RecipeeAPI.Models;

namespace RecipeeAPI.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private RecipeeContext _context;
        private IMapper _mapper;

        public RecipeService(RecipeeContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetRecipeDTO>> AddRecipe(AddRecipeDTO newRecipe)
        {
            ServiceResponse<GetRecipeDTO> response = new ServiceResponse<GetRecipeDTO>();
            Recipe recipe = _mapper.Map<Recipe>(newRecipe);
            recipe.Creator = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetRecipeDTO>(recipe);
            return response;
        }

        public async Task<ServiceResponse<List<GetRecipeDTO>>> GetAllRecipes()
        {
            ServiceResponse<List<GetRecipeDTO>> response = new ServiceResponse<List<GetRecipeDTO>>();
            List<Recipe> dbRecipes = await _context.Recipes
                .Where(r => r.UserId == GetUserId())
                .Include(r => r.Ingredients)
                .Include(r => r.Methods)
                .AsNoTracking()
                .ToListAsync();
            response.Data = (dbRecipes.Select(r => _mapper.Map<GetRecipeDTO>(r))).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetRecipeDTO>> GetRecipeById(int id)
        {
            ServiceResponse<GetRecipeDTO> response = new ServiceResponse<GetRecipeDTO>();
            Recipe recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Methods)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            response.Data = _mapper.Map<GetRecipeDTO>(recipe);
            return response;
        }

        private int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}