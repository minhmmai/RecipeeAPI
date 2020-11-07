using Azure.Core;
using RecipeeAPI.DTOs.Auth;
using RecipeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(RegisterUserDTO registerUserDTO);
        Task<ServiceResponse<AccessToken>> Login(LoginUserDTO loginUserDTO);
    }
}
