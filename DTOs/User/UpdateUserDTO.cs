using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.DTOs.User
{
    /// <summary>
    /// DTO for updating user's info
    /// </summary>
    public class UpdateUserDTO
    {
        /// <summary>
        /// New user's first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// New user's last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// New user's email address
        /// </summary>
        public string Email { get; set; }
    }
}
