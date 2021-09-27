namespace Engenious.Core.Managers
{
    public class AccessTokenHolder
    {
        private const string TokenKey = "Token";
    
        public string CurrentToken
        {
            get => PrefsUtils.GetString(TokenKey);
            set => PrefsUtils.SetString(TokenKey, value);
        }
    }
}