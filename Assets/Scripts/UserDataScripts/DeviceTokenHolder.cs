namespace Assets.Scripts.UserDataScripts
{
    public static class DeviceTokenHolder
    {
        private const string DeviceTokenKey = "DeviceToken";
    
        public static string DeviceToken
        {
            get => PrefsUtils.GetString(DeviceTokenKey);
            set => PrefsUtils.SetString(DeviceTokenKey, value);
        }

        public static void ClearUserId()
        {
            DeviceToken = string.Empty;
        }

        public static bool IsDeviceTokenEmpty()
        {
            return DeviceToken.Equals(string.Empty);
        }
    }
}