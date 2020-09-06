using RecipeeAPI.DTOs.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.DTOs.User
{
    /// <summary>
    /// DTO for retreiving user's info
    /// </summary>
    public class GetUserDTO
    {
        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// User's email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// List of user's recipes
        /// </summary>
        public List<GetRecipeDTO> Recipes { get; set; }
    }
}
