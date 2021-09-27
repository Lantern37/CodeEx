using System;
using Assets.Scripts.UserDataScripts;
using Engenious.Core.Managers;
using Engenious.Core.Managers.Requests;
using Engenious.MainScene.Models;
using Firebase.Auth;
using UnityEngine;

namespace Engenious.MainScene
{
    public interface IProvidePhoneModel
    { 
        PhoneAuthoorizedStruct Phone { get; }
        bool IsInited { get; }
        event Action<string> FailVerification;
        event Action<string,string, ForceResendingToken> SuccessVerification;
        event Action<PhoneAuthoorizedStruct> PhoneAuthoorized;
        event Action FailPhoneAuthoorized;
        void LogPhoneNumber(string phoneNumber);
        void AuthoorizeWithPhone(string verificationId, string verificationCode);        
        void Initialize();
        void UnSubscribe();
    }

    public class ProvidePhoneModel : IProvidePhoneModel
    {
        private INetworkUserRequests _userRequests;

        private readonly IUserDataController _userData;

        //private readonly ILogInModel _logInmMdel;
        public PhoneAuthoorizedStruct Phone { get; private set; }
        public event Action<string> FailVerification;
        public event Action<string,string, ForceResendingToken> SuccessVerification;
        public event Action<string> CodeAutoRetrievalTimeOut;
        public event Action<PhoneAuthoorizedStruct> PhoneAuthoorized;
        public event Action FailPhoneAuthoorized;

        private uint _phoneAuthTimeoutMs = 60000;

        private FirebaseAuth _auth;

        private PhoneAuthProvider _provider;

        public bool IsInited { get; private set; }

        public void Initialize()
        {
            if (!IsInited)
            {
                _auth = FirebaseAuth.DefaultInstance;
                _provider = PhoneAuthProvider.GetInstance(_auth);
                
                IsInited = true;
            }
        }

        public ProvidePhoneModel(INetworkUserRequests userRequests, IUserDataController userData)
        {
            _userRequests = userRequests;
            _userData = userData;
        }
        
        public void LogPhoneNumber(string phoneNumber)
        {
            _provider.VerifyPhoneNumber(phoneNumber, _phoneAuthTimeoutMs, null,
                verificationCompleted: (credential) =>
                {
                    // Auto-sms-retrieval or instant validation has succeeded (Android only).
                    // There is no need to input the verification code.
                    // `credential` can be used instead of calling GetCredential().
                },
                verificationFailed: (error) =>
                {
                    // The verification code was not sent.
                    // `error` contains a human readable explanation of the problem.
                    Debug.LogError("verificationFailed = "  + error);
                    FailVerification?.Invoke(error);
                },
                codeSent: (id, token) =>
                {
                    // Verification code was successfully sent via SMS.
                    // `id` contains the verification id that will need to passed in with
                    // the code from the user when calling GetCredential().
                    // `token` can be used if the user requests the code be sent again, to
                    // tie the two requests together.
                    SuccessVerification?.Invoke(id, phoneNumber ,token);
                },
                codeAutoRetrievalTimeOut: (id) =>
                {
                    // Called when the auto-sms-retrieval has timed out, based on the given
                    // timeout parameter.
                    // `id` contains the verification id of the request that timed out.
                    CodeAutoRetrievalTimeOut?.Invoke(id);
                });
        }

        public async void AuthoorizeWithPhone(string verificationId, string verificationCode)
        {
            Debug.Log("AuthoorizeWithPhone");

            Credential credential = _provider.GetCredential(verificationId, verificationCode);

            FirebaseUser newUser = null;

            await _auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " +
                                   task.Exception);
                    FailPhoneAuthoorized?.Invoke();
                    return;
                }

                newUser = task.Result;
                Debug.Log("User signed in successfully");
                // This should display the phone number.
                Debug.Log("Phone number: " + newUser.PhoneNumber);
                // The phone number providerID is 'phone'.
                Debug.Log("Phone provider ID: " + newUser.ProviderId);

                PhoneAuthoorizedStruct phone = new PhoneAuthoorizedStruct(newUser.PhoneNumber, verificationCode);
                Phone = phone;
            });


            var phoneRequest = new VerifyPhoneRequest();
            phoneRequest.Phone = newUser.PhoneNumber;

            Debug.Log("phoneRequest = " + phoneRequest.Phone + " userid " + _userData.UserData.Id);

            _userData.UserDataHolder.SetPhone(Phone.Phone);
            _userData.SaveData();
            
            await _userRequests.PutPhoneNumber(phoneRequest, _userData.UserData.Id);

            PhoneAuthoorized?.Invoke(Phone);
        }

        public void UnSubscribe()
        {
            FailVerification = null;
            SuccessVerification = null;
            CodeAutoRetrievalTimeOut = null;
            PhoneAuthoorized = null;
        }
    }
}