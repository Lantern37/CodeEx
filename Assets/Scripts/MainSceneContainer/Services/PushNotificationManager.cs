using System;
using Firebase.Messaging;

namespace Engenious.MainScene.Services
{
    public interface IPushNotificationManager
    {
        event Action<TokenReceivedEventArgs> TokenReceived;
        event Action<MessageReceivedEventArgs> MessageReceived;
        void Start();
        void Stop();
    }

    public class PushNotificationManager : IPushNotificationManager
    {
        public event Action<TokenReceivedEventArgs> TokenReceived;
        public event Action<MessageReceivedEventArgs> MessageReceived;

        public void Start()
        {
            Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
            Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        }

        public void Stop()
        {
            Firebase.Messaging.FirebaseMessaging.TokenReceived -= OnTokenReceived;
            Firebase.Messaging.FirebaseMessaging.MessageReceived -= OnMessageReceived;
        }
        
        private void OnTokenReceived(object sender, TokenReceivedEventArgs token) 
        {
            UnityEngine.Debug.LogError("Received Registration Token: " + token.Token);
            TokenReceived?.Invoke(token);
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e) 
        {
            UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
            MessageReceived?.Invoke(e);
        }
    }
}