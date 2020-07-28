using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeeAPI.DTOs.User;
using RecipeeAPI.Models;
using RecipeeAPI.Services;
using RecipeeAPI.Services.Auth;

namespace RecipeeAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        [Route("~/register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            ServiceResponse<int> response = await _auth.Register(
                new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                }, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Route("~/login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            ServiceResponse<string> response = await _auth.Login(request.Email, request.Password);
            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
