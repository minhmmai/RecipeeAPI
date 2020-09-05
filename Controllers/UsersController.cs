using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeeAPI.Data;
using RecipeeAPI.DTOs.User;
using RecipeeAPI.Services;
using RecipeeAPI.Services.UserService;

namespace RecipeeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private RecipeeContext _context;
        private IUserService _userService;

        public UsersController(RecipeeContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (await _context.Users.AnyAsync(u => u.Id == id))
            {
                return Ok(await _userService.GetUserById(id));
            }

            return BadRequest();
        }

        [HttpPost("me")]
        public async Task<IActionResult> UpdateDetails(UpdateUserDTO updatedDetails)
        {
            if (updatedDetails == null)
            {
                return BadRequest();
            }

            return Ok(await _userService.UpdateUserDetails(updatedDetails));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetUserDetails()
        {
            return Ok(await _userService.GetDetails());
        }
    }
}
