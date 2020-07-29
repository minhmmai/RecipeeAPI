using System.Linq;
using AutoMapper;
using RecipeeAPI.DTOs.Recipe;
using RecipeeAPI.DTOs.User;
using RecipeeAPI.Models;

namespace RecipeeAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Recipe, GetRecipeDTO>();
            CreateMap<AddRecipeDTO, Recipe>();
        }
    }
}