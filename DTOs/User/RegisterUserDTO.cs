using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.DTOs.User
{
    /// <summary>
    /// Register user DTO
    /// </summary>
    public class RegisterUserDTO
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
        /// User's password
        /// </summary>
        public string Password { get; set; }
    }
}
