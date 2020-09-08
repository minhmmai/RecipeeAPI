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
using RecipeeAPI.Services.UserService;

namespace RecipeeAPI.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeeContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public RecipeService(RecipeeContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<ServiceResponse<GetRecipeDTO>> AddRecipe(AddRecipeDTO newRecipe)
        {
            ServiceResponse<GetRecipeDTO> response = new ServiceResponse<GetRecipeDTO>();

            try
            {
                Recipe recipe = _mapper.Map<Recipe>(newRecipe);
                recipe.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == _userService.GetUserId());
                await _context.Recipes.AddAsync(recipe);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetRecipeDTO>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetRecipeDTO>>> GetAllRecipes()
        {
            ServiceResponse<List<GetRecipeDTO>> response = new ServiceResponse<List<GetRecipeDTO>>();

            try
            {
                List<Recipe> dbRecipes = await _context.Recipes
                    .Where(r => r.UserId == _userService.GetUserId())
                    .Include(r => r.Ingredients)
                    .Include(r => r.Methods)
                    .AsNoTracking()
                    .ToListAsync();

                response.Data = (dbRecipes.Select(r => _mapper.Map<GetRecipeDTO>(r))).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

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

                if (recipe.UserId == _userService.GetUserId())
                {
                    recipe.Name = updatedRecipe.Name;
                    recipe.Description = updatedRecipe.Description;
                    recipe.Serves = updatedRecipe.Serves;

                    if (updatedRecipe.Ingredients.Any())
                    {
                        //Remove an existing ingredient if it is not present in the DTO
                        foreach (var ingredient in recipe.Ingredients)
                        {
                            if (!updatedRecipe.Ingredients.Any(i => i.Id == ingredient.Id))
                            {
                                _context.Ingredients.Remove(ingredient);
                            }
                        }

                        //Update each existing ingredient with the one from the DTO
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
                        //Remove existing method if it is not present in the DTO
                        foreach (var method in recipe.Methods)
                        {
                            if (!updatedRecipe.Methods.Any(m => m.Index == method.Id))
                            {
                                _context.Methods.Remove(method);
                            }
                        }

                        //Update each existing method with the one from the DTO
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