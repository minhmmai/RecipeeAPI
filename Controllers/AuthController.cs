using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeeAPI.DTOs.User;
using RecipeeAPI.Models;
using RecipeeAPI.Services;
using RecipeeAPI.Services.AuthService;

namespace RecipeeAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO request)
        {
            ServiceResponse<string> response = await _auth.Register(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO request)
        {
            ServiceResponse<string> response = await _auth.Login(request);
            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
