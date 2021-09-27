using System;
using Assets.Scripts.MainWindows;
using Engenious.Core.Managers;
using Engenious.Core.Managers.Requests;
using Engenious.MainScene.ViewModels;

namespace Assets.Scripts.MainSceneContainer.ViewModels
{
    public class SupportVM : BaseVM<SupportWindow>
    {
        private ISupportNetwork _supportNetwork;

        public SupportVM(ISupportNetwork supportNetwork)
        {
            _supportNetwork = supportNetwork;
        }

        public async void SendMessage(string title, string message, Action<SupportResponce> success = null, Action fail = null)
        {
            SupportRequest request = new SupportRequest()
            {
                Title = title,
                Message = message
            };

            var response = await _supportNetwork.PostSupportQuestion(request);

            if (response == null)
            {
                fail?.Invoke();
            }
            else
            {
                success?.Invoke(response);                
            }
        }
    }
}