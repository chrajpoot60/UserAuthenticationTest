using Microsoft.AspNetCore.Mvc;
using UserAuthenticationTest.Models;
using UserAuthenticationTest.ViewModels;

namespace UserAuthenticationTest.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GenerateJwtTokenAsync(string loginId, string password);
        Task<List<UserModel>> GetUserDetailsAsync();
        Task<ObjectResult> AddUserAsync(UserViewModel user);
    }
}
