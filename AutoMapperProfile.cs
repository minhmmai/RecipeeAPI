using System.Linq;
using AutoMapper;
using RecipeeAPI.DTOs.Ingredient;
using RecipeeAPI.DTOs.Method;
using RecipeeAPI.DTOs.Recipe;
using RecipeeAPI.Models;

namespace RecipeeAPI
{
    public class AutoMapperProfile : Profile
    {
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
        }
    }
}