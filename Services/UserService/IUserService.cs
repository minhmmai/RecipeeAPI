﻿using RecipeeAPI.DTOs.User;
using System.Threading.Tasks;

namespace RecipeeAPI.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<GetUserDTO>> GetUserById(int id);
        Task<ServiceResponse<GetUserDTO>> UpdateUserDetails(UpdateUserDTO updatedDetails);
        Task<ServiceResponse<GetUserDTO>> GetDetails();
    }
}