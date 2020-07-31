﻿using System;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            try
            {
                response = await _auth.Register(request);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            ServiceResponse<string> response = await _auth.Login(request.Email, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
