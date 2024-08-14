namespace UserAuthenticationTest.Helpers
{
    public static class PasswordHasherHelper
    {
        public static string GenerateHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool IsValidPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
