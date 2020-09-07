using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RecipeeAPI.Data;
using RecipeeAPI.DTOs.User;
using RecipeeAPI.Models;
using RecipeeAPI.Services.UserService;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RecipeeAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly RecipeeContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public AuthService(RecipeeContext context, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _context = context;
            _userManager = userManager;
            _userService = userService;
        }

        public Task<ServiceResponse<string>> Login(LoginUserDTO loginUserDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<string>> Register(RegisterUserDTO registerUserDTO)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            try
            {
                if(await _userService.UserExist(registerUserDTO.Email))
                {
                    response.Success = false;
                    response.Message = "User already exist";
                    return response;
                }

                ApplicationUser newUser = new ApplicationUser
                {
                    UserName = registerUserDTO.Email,
                    FirstName = registerUserDTO.FirstName,
                    LastName = registerUserDTO.LastName,
                    Email = registerUserDTO.Email
                };

                var result = await _userManager.CreateAsync(newUser, registerUserDTO.Password);

                if (!result.Succeeded)
                {
                    response.Success = false;
                    response.Message = "Failed to register user";
                    return response;
                }

                ApplicationUser createdUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerUserDTO.Email);

                await _userManager.AddClaimAsync(createdUser, new Claim(ClaimTypes.Role, "member"));

                response.Data = createdUser.Id;
                response.Message = "Successfully registered user";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
