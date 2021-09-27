using System;

namespace Engenious.Core.Managers
{
    public class UserIdHolder
    {
        private const string UserIdKey = "UserId";
    
        public string UserId
        {
            get => PrefsUtils.GetString(UserIdKey);
            set => PrefsUtils.SetString(UserIdKey, value);
        }

        public void ClearUserId()
        {
            UserId = String.Empty;
        }

        public bool IsUserIdEmpty()
        {
            return UserId.Equals(String.Empty);
        }
    }
}