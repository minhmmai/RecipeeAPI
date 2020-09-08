using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RecipeeAPI.Data;
using RecipeeAPI.DTOs.User;
using RecipeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecipeeAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly RecipeeContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(RecipeeContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<GetUserDTO>> GetDetails()
        {
            ServiceResponse<GetUserDTO> response = new ServiceResponse<GetUserDTO>();
            ApplicationUser user = await _context.Users
                .Include(u => u.Recipes)
                .FirstOrDefaultAsync(u => u.Id == GetUserId());
            response.Data = _mapper.Map<GetUserDTO>(user);
            return response;
        }

        public async Task<ServiceResponse<GetUserDTO>> GetUserById(string id)
        {
            ServiceResponse<GetUserDTO> response = new ServiceResponse<GetUserDTO>();
            try
            {
                ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
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
                ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
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

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue("user_id");
        }

        public async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
        }
    }
}
