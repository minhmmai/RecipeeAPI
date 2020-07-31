using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RecipeeAPI.Common;
using RecipeeAPI.Data;
using RecipeeAPI.DTOs.User;
using RecipeeAPI.Models;
using System;
using System.Threading.Tasks;

namespace RecipeeAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private RecipeeContext _context;
        private IHttpContextAccessor _httpContextAccessor;

        public UserService(RecipeeContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<GetUserDTO>> GetDetails()
        {
            ServiceResponse<GetUserDTO> response = new ServiceResponse<GetUserDTO>();
            User user = await _context.Users
                .Include(u => u.Recipes)
                .FirstOrDefaultAsync(u => u.Id == UserHelper.GetUserId(_httpContextAccessor));
            response.Data = _mapper.Map<GetUserDTO>(user);
            return response;
        }

        public async Task<ServiceResponse<GetUserDTO>> GetUserById(int id)
        {
            ServiceResponse<GetUserDTO> response = new ServiceResponse<GetUserDTO>();
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                response.Data = _mapper.Map<GetUserDTO>(user);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetUserDTO>> UpdateUserDetails(UpdateUserDTO updatedDetails)
        {
            ServiceResponse<GetUserDTO> response = new ServiceResponse<GetUserDTO>();
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserHelper.GetUserId(_httpContextAccessor));
                user.FirstName = updatedDetails.FirstName;
                user.LastName = updatedDetails.LastName;
                user.Email = updatedDetails.Email;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetUserDTO>(user);
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
