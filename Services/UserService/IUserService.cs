using RecipeeAPI.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<GetUserDTO>> GetUserById(string id);
        Task<ServiceResponse<GetUserDTO>> UpdateUserDetails(UpdateUserDTO updatedDetails);
        Task<ServiceResponse<GetUserDTO>> GetDetails();
        Task<bool> UserExist(string email);
    }
}
