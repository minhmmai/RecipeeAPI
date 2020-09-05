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
        private IConfiguration _configuration;
        private RecipeeContext _context;

        public AuthService(RecipeeContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
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
