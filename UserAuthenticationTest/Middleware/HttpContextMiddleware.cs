using UserAuthenticationTest.DAL;
using UserAuthenticationTest.Helpers;
using UserAuthenticationTest.Models;

namespace UserAuthenticationTest.Middleware
{
    public class HttpContextMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly AppDbContext _appDbContext;
        public HttpContextMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task Invoke(HttpContext context, AppDbContext appDbContext)
        {
            if(!appDbContext.Users.Any(x => x.user_id == 1))
            {
                appDbContext.Add<UserModel>(new UserModel
                {
                    user_id = 1,
                    first_name = "TestFirst",
                    last_name = "TestLast",
                    login_id = "firstlasttest@gmail.com",
                    created_on = DateTime.Now,
                    pass = PasswordHasherHelper.GenerateHashedPassword("Lastfirst123@")
                });
                appDbContext.SaveChanges();
            }
            var token = context.Request.Headers["Authorization"];
            await _next.Invoke(context);
        }
    }
}
