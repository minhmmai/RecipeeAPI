using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RecipeeAPI.Data;
using RecipeeAPI.DTOs.User;
using RecipeeAPI.Models;
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
        private RecipeeContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(RecipeeContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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

                await _userManager.AddToRoleAsync(createdUser, "member");

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

        public Task<bool> UserExist(string email)
        {
            throw new NotImplementedException();
        }
    }
}
