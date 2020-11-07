using System.Linq;
using AutoMapper;
using RecipeeAPI.DTOs.Ingredient;
using RecipeeAPI.DTOs.Method;
using RecipeeAPI.DTOs.Recipe;
using RecipeeAPI.DTOs.User;
using RecipeeAPI.Models;

namespace RecipeeAPI
{
    /// <summary>
    /// Class to create AutoMapper profile
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// AutoMapper profile
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Recipe, GetRecipeDTO>();
            CreateMap<AddRecipeDTO, Recipe>();
            CreateMap<Ingredient, GetIngredientDTO>();
            CreateMap<AddIngredientDTO, Ingredient>();
            CreateMap<Method, GetMethodDTO>();
            CreateMap<AddMethodDTO, Method>();
            CreateMap<GetIngredientDTO, Ingredient>();
            CreateMap<GetMethodDTO, Method>();
            CreateMap<AppUser, GetUserDTO>();
        }
    }
}