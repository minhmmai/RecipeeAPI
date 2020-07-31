using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RecipeeAPI.Common;
using RecipeeAPI.Data;
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
            recipe.Creator = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserHelper.GetUserId(_httpContextAccessor));
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetRecipeDTO>(recipe);
            return response;
        }

        public async Task<ServiceResponse<List<GetRecipeDTO>>> GetAllRecipes()
        {
            ServiceResponse<List<GetRecipeDTO>> response = new ServiceResponse<List<GetRecipeDTO>>();
            List<Recipe> dbRecipes = await _context.Recipes
                .Where(r => r.UserId == UserHelper.GetUserId(_httpContextAccessor))
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

        public async Task<ServiceResponse<GetRecipeDTO>> UpdateRecipe(int id, UpdateRecipeDTO updatedRecipe)
        {
            ServiceResponse<GetRecipeDTO> response = new ServiceResponse<GetRecipeDTO>();
            try
            {
                Recipe recipe = await _context.Recipes
                    .Include(r => r.Ingredients)
                    .Include(r => r.Methods)
                    .FirstOrDefaultAsync(r => r.Id == id);
                if (recipe.UserId == UserHelper.GetUserId(_httpContextAccessor))
                {
                    recipe.Name = updatedRecipe.Name;
                    recipe.Description = updatedRecipe.Description;
                    recipe.Serves = updatedRecipe.Serves;

                    if (updatedRecipe.Ingredients.Any())
                    {
                        foreach (var ingredient in recipe.Ingredients)
                        {
                            if (!updatedRecipe.Ingredients.Any(c => c.Id == ingredient.Id))
                            {
                                _context.Ingredients.Remove(ingredient);
                            }
                        }

                        foreach (var ingredient in updatedRecipe.Ingredients)
                        {
                            var ingredientToUpdate = recipe.Ingredients.FirstOrDefault(i => i.Id == ingredient.Id);

                            if (ingredientToUpdate != null)
                            {
                                // Update ingredient
                                ingredientToUpdate.Name = ingredient.Name;
                                ingredientToUpdate.Quantity = ingredient.Quantity;
                                ingredientToUpdate.Unit = ingredient.Unit;
                                _context.Ingredients.Update(ingredientToUpdate);
                            }
                            else
                            {
                                // Insert ingredient
                                var newIngredient = new Ingredient
                                {
                                    Name = ingredient.Name,
                                    Quantity = ingredient.Quantity,
                                    Unit = ingredient.Unit
                                };
                                recipe.Ingredients.Add(newIngredient);
                            }
                        }
                    }

                    if (updatedRecipe.Methods.Any())
                    {
                        foreach (var method in recipe.Methods)
                        {
                            if (!updatedRecipe.Methods.Any(m => m.Index == method.Id))
                            {
                                _context.Methods.Remove(method);
                            }
                        }

                        foreach (var method in updatedRecipe.Methods)
                        {
                            var methodToUpdate = recipe.Methods.FirstOrDefault(m => m.Id == method.Id);

                            if (methodToUpdate != null)
                            {
                                // Update method
                                methodToUpdate.Index = method.Index;
                                methodToUpdate.Detail = method.Detail;
                                _context.Methods.Update(methodToUpdate);
                            }
                            else
                            {
                                // Insert method
                                var newMethod = new Method
                                {
                                    Index = method.Index,
                                    Detail = method.Detail
                                };
                                recipe.Methods.Add(newMethod);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    response.Data = _mapper.Map<GetRecipeDTO>(recipe);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Recipe not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}