using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserAuthenticationTest.DAL;
using UserAuthenticationTest.Helpers;
using UserAuthenticationTest.Models;
using UserAuthenticationTest.Services.Interfaces;
using UserAuthenticationTest.ViewModels;

namespace UserAuthenticationTest.Services
{
    public class UserService: IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;
        public UserService(IServiceProvider serviceProvider, AppDbContext appDbContext)
        {
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
            _appDbContext = appDbContext;
        }
        
        public async Task<string> GenerateJwtTokenAsync(string loginId, string password)
        {
            UserModel userModel; 
            if (string.IsNullOrEmpty(loginId) || string.IsNullOrEmpty(password))
                return null;
            var users = await GetUserDetailsAsync();
            var loginUser = users.Where(x => x.login_id == loginId)?.FirstOrDefault();
            if(loginUser == null)
                return null;
            if (!PasswordHasherHelper.IsValidPassword(password, loginUser.pass))
                return null;
                
            var securrityKey = _configuration["TokenSettings:SecurityKey"];
            var issuer = _configuration["TokenSettings:Issuer"];
            var audience = _configuration["TokenSettings:Audience"];
            return TokenGeneratorHelper.GenerateJwtToken(loginUser, securrityKey, issuer, audience);
        }

        public async Task<ObjectResult> AddUserAsync(UserViewModel user)
        {
            if(user != null)
            {
                if (_appDbContext.Users.Any(x => x.user_id == user.user_id))
                    return new BadRequestObjectResult(new { error = "Data Alredy exist for the user_id" });
                var seriliazeUser = JsonConvert.SerializeObject(user);
                var userModel = JsonConvert.DeserializeObject<UserModel>(seriliazeUser);
                await _appDbContext.AddAsync<UserModel>(userModel);
                _appDbContext.SaveChanges();
                return new OkObjectResult("Inserted");
            }
            return new BadRequestObjectResult(new { error = "Input is null or empty"});
        }

        public async Task<List<UserModel>> GetUserDetailsAsync()
        {
            //List<UserModel> userModels = new List<UserModel>()
            //{
            //    new UserModel()
            //    {
            //        user_id = 1,
            //        first_name = "TestFirst",
            //        last_name = "TestLast",
            //        login_id = "firstlasttest@gmail.com",
            //        created_on = DateTime.Now,
            //        pass = PasswordHasherHelper.GenerateHashedPassword("Lastfirst123@")
            //    }
            //};
            var userModels = _appDbContext.Users.ToList();
            return userModels;
        }
    }
}
