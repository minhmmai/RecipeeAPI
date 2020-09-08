using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeeAPI.DTOs.User;
using RecipeeAPI.Models;
using RecipeeAPI.Services;
using RecipeeAPI.Services.AuthService;

namespace RecipeeAPI.Controllers
{
    /// <summary>
    /// Controller for all methods associated with authentication.
    /// </summary>
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _auth;

        /// <summary>
        /// Constructor that initializes the authentication controller.
        /// </summary>
        /// <param name="auth">The services available to authentication.</param>
        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        /// <summary>
        /// Register a new user account.
        /// </summary>
        /// <param name="request">The DTO for registering a user account.</param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO request)
        {
            ServiceResponse<string> response = await _auth.Register(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        ///     Login a user
        /// </summary>
        /// <param name="request">The DTO for logging in a user.</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO request)
        {
            ServiceResponse<AccessToken> response = await _auth.Login(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
