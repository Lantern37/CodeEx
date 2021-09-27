using System;
using Engenious.Core.Managers;
using Newtonsoft.Json;

namespace Assets.Scripts.UserDataScripts
{
    [Serializable]
    public class SavedUserData
    {
        public int    Id;
        public string Email;
        public string Phone;
        public string Name;
        public string FormattedAddress;
        public float Lat;
        public float Lon;

        public bool TutorWasShowed;
        //public Result Address;

        public string DeviceToken
        {
            get
            {
                return DeviceTokenHolder.DeviceToken;
            }

            set
            {
                DeviceTokenHolder.DeviceToken = value;
            }
        }

        public void Clear()
        {
            Id = 0;
            Email = String.Empty;
            Phone = String.Empty;
            Name = String.Empty;
            //Address = String.Empty;
            //DeviceToken = String.Empty;
            //Address = new Result();
            FormattedAddress = String.Empty;
            Lat = 0;
            Lon = 0;
            TutorWasShowed = false;
        }

        public void SetAddress(Result address)
        {
            FormattedAddress = address.FormattedAddress;
            Lat = (float)address.Geometry.Location.Lat;
            Lon = (float)address.Geometry.Location.Lng;
        }

        public Result GetResultAddress()
        {
            Result res = new Result();

            res.Geometry = new Geometry()
            {
                Location = new Location()
                {
                    Lat = Lat,
                    Lng = Lon
                },
            };

            res.FormattedAddress = FormattedAddress;

            return res;
        }
        
        public override string ToString()
        {
            return "Id = " + Id + " phone = " + Phone + " address = " + FormattedAddress + " DeviceToken " + DeviceToken;
        }
    }
}