using System.Text.RegularExpressions;

namespace Engenious.MainScene.Services
{
    public interface IValidationService
    {
        bool IsEmailValid(string email);
        bool IsPhoneValid(string number);
    }

    public class ValidationService : IValidationService
    {
        private const string MatchEmailPattern = 
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";
        
        public bool IsEmailValid(string email)
        {
            if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
            else return false;
        }

        public bool IsPhoneValid(string number)
        {
            return true;
        }
    }
}