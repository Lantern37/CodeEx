using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Engenious.Core.Managers
{
    public interface IAddressRequests
    {
        Task<GooglePlacesResponse> GetAddresses(string userInput, Action<float> progress = null);
    }

    public class AddressRequests : IAddressRequests
    {
        private NetworkManager _manager;

        private const string API_KEY = "AIzaSyB031ELDIJ5uHRGHcFrSfmD78156lOgErg"; 
        
        private string _language = "en"; //todo localization 
        
        public AddressRequests(){}
        public AddressRequests(NetworkManager manager)
        {
            _manager = manager;
        }
        
        public async Task<GooglePlacesResponse> GetAddresses(string userInput, Action<float> progress = null)
        {
            var requestString = _manager.Config.GetAddress +"query=" +userInput + "&key="+API_KEY+"&language="+_language;
            var request = await _manager.Request<GooglePlacesResponse>(requestString, 
                NetworkManager.RequestTypes.Get, 
                string.Empty,
                new Dictionary<string, string>
                {
                    {"Content-Type", "application/json; charset=utf-8"},
                    {"Accept", "application/json"},
                    {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
                }, false, false, progress);
            return request;
        }
    }
}