using RecipeeAPI.DTOs.User;
using System.Threading.Tasks;

namespace RecipeeAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegisterDTO userRegisterDTO);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExist(string email);
    }
}
