namespace Receptsamlingen.Mvc.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? LoggedIn { get; set; }
    }
}