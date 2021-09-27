namespace Engenious.MainScene.Models
{
    public class LogInModelUser
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public LogInModelUser(string  email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}