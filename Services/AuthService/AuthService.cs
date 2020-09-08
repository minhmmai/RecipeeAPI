using AutoMapper;
using Azure.Core;
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
        private readonly IConfiguration _configuration;

        public AuthService(RecipeeContext context, UserManager<ApplicationUser> userManager, IUserService userService, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<AccessToken>> Login(LoginUserDTO loginUserDTO)
        {
            ServiceResponse<AccessToken> response = new ServiceResponse<AccessToken>();

            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(loginUserDTO.Email);
                Boolean checkLoginDetails = await _userManager.CheckPasswordAsync(user, loginUserDTO.Password);

                if (user == null || !checkLoginDetails)
                {
                    response.Success = false;
                    response.Message = "Invalid login details";
                    return response;
                }

                response.Data = CreateToken(user);
                response.Message = "Login successful";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<string>> Register(RegisterUserDTO registerUserDTO)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            try
            {
                if (await _userService.UserExist(registerUserDTO.Email))
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

        public AccessToken CreateToken(ApplicationUser user)
        {
            var userRoles = _userManager.GetClaimsAsync(user).Result.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            DateTime dateTimeNow = DateTime.Now;
            DateTime dateTimeExpiry = dateTimeNow.AddMinutes(60.0);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("user_id", user.Id)
            };

            foreach (var item in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["AuthSettings:SecretKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                _configuration["AuthSettings:ValidIssuer"],
                _configuration["AuthSettings:ValidAudience"],
                claims,
                dateTimeNow,
                dateTimeExpiry,
                signingCredentials
            );

            JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            return new AccessToken(_jwtSecurityTokenHandler.WriteToken(jwtSecurityToken), dateTimeExpiry);
        }
    }
}
