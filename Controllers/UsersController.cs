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
    /// <summary>
    /// Controller for all methods associated with a user account.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private RecipeeContext _context;
        private IUserService _userService;
        /// <summary>
        /// Constructor that initializes the user controller.
        /// </summary>
        /// <param name="context">The Recipee database context.</param>
        /// <param name="userService">The services available to a user account.</param>
        public UsersController(RecipeeContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        /// <summary>
        /// Get a user account information using its Id.
        /// </summary>
        /// <param name="id">The Id of the user account.</param>
        /// <returns>The information related to the user account.</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (await _context.Users.AnyAsync(u => u.Id == id))
            {
                return Ok(await _userService.GetUserById(id));
            }

            return BadRequest();
        }

        /// <summary>
        /// Update account details of the current user.
        /// </summary>
        /// <param name="updatedDetails"></param>
        /// <returns>User account with the updated details.</returns>
        [HttpPost("me")]
        public async Task<IActionResult> UpdateDetails(UpdateUserDTO updatedDetails)
        {
            if (updatedDetails == null)
            {
                return BadRequest();
            }

            return Ok(await _userService.UpdateUserDetails(updatedDetails));
        }

        /// <summary>
        /// Get the current logged in user account information.
        /// </summary>
        /// <returns>The user account information.</returns>
        [HttpGet("me")]
        public async Task<IActionResult> GetUserDetails()
        {
            return Ok(await _userService.GetDetails());
        }
    }
}
