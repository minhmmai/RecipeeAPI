using RecipeeAPI.DTOs.FbAuth;
using RecipeeAPI.Models;
using RecipeeAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.Services.FbAuthService
{
    public interface IFbAuthService
    {
        Task<FbUser> GetFbUserAsync(UserTokenDTO userToken);
    }
}
