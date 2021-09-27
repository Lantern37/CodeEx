using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engenious.Core.Managers.Requests;
using Newtonsoft.Json;

namespace Engenious.Core.Managers
{
    public interface ISupportNetwork
    {
        Task<SupportResponce> PostSupportQuestion(SupportRequest message, Action<float> progress = null);
    }

    public class SupportNetwork : ISupportNetwork
    {
        private NetworkManager _manager;

        public SupportNetwork(NetworkManager manager)
        {
            _manager = manager;
        }

        public async Task<SupportResponce> PostSupportQuestion(SupportRequest message, Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL + _manager.Config.PostSupport;
            var body = JsonConvert.SerializeObject(message);
            //Debug.Log("Body Login " + body);

            //var bodyString = "{\"email\":\"myTest@eng.ua\",\"password\":\"test\"}";
            //Debug.Log("body login= "+ body.ToString());

            var request = await _manager.Request<SupportResponce>(requestString,
                NetworkManager.RequestTypes.Post,
                body,
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