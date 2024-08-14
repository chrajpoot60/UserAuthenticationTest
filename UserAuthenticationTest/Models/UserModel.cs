namespace UserAuthenticationTest.Models
{
    public class UserModel
    {
        public int user_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? login_id { get; set; }
        public string? pass { get; set; }
        public DateTime created_on { get; set; }
        public UserModel()
        {
            created_on = DateTime.Now;  
        }
    }
}
