namespace Engenious.MainScene.SignUp
{
    public class SignUpModelUser
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public SignUpModelUser(string  email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}