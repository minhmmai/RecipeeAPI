using RecipeeAPI.DTOs.Recipe;
using System.Collections.Generic;

namespace RecipeeAPI.DTOs.User
{
    public class GetUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<GetRecipeDTO> Recipes { get; set; }
    }
}
