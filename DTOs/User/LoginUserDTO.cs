namespace RecipeeAPI.DTOs.User
{
    /// <summary>
    /// DTO for logging in a user
    /// </summary>
    public class LoginUserDTO
    {
        /// <summary>
        /// User's email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User account password
        /// </summary>
        public string Password { get; set; }
    }
}