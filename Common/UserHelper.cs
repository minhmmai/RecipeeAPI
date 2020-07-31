using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RecipeeAPI.Common
{
    public static class UserHelper
    {
        public static int GetUserId(IHttpContextAccessor httpContextAccessor)
        {
            return int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
