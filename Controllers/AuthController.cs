using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RecipeeAPI.DTOs.Auth;
using RecipeeAPI.DTOs.FbAuth;
using RecipeeAPI.Models;
using RecipeeAPI.Services;
using RecipeeAPI.Services.AuthService;
using RecipeeAPI.Services.FbAuthService;

namespace RecipeeAPI.Controllers
{
    /// <summary>
    /// Controller for all methods associated with authentication.
    /// </summary>
    [ApiController]
    [Route("api/")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IFbAuthService _fbAuth;

        /// <summary>
        /// Constructor that initializes the authentication controller.
        /// </summary>
        /// <param name="auth">The services available to authentication.</param>
        public AuthController(IAuthService auth, IFbAuthService fbAuth)
        {
            _auth = auth;
            _fbAuth = fbAuth;
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

        /// <summary>
        ///     Login a user through Facebook
        /// </summary>
        /// <param name="request">User's access token retrieved from Facebook</param>
        /// <returns></returns>
        [HttpPost("login/fb")]
        public async Task<IActionResult> LoginFb(UserTokenDTO request)
        {
            var response = await _fbAuth.GetFbUserAsync(request);

            if (response == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
