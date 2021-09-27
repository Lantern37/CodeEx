using System.Runtime.CompilerServices;
using Engenious.Core.Managers;
using Engenious.Core.Managers.Requests;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Test
{
    public class TestNetwork : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_DebugLogText;
        private NetworkManager _network = new NetworkManager();

        [SerializeField] private PutUserRequest req;
        [SerializeField] private GetUserRequest _getUserReq;
        [SerializeField] private LoginUserRequest _loginReq;

        [SerializeField] private NetworkConfig _networkConfig;

        private ISighnUPView m_SignUpView;

        public ISighnUPView SignUpView
        {
            set => m_SignUpView = value;
        }
        
        private ILoginUPView m_LoginView;

        public ILoginUPView LoginView
        {
            set => m_LoginView = value;
        }

        // private IRegistrationHandler m_CurrentRegistrationHandler;
        
        private void Start()
        {
            _network.Config = _networkConfig;
        }
        private async void GetRequest()
        {
            var resp = await _network.GetUser(_getUserReq);
            Debug.Log("GetResponce = " + resp.Email+ " " + resp.Password);
        }

        private async void PutRequest()
        {
            
            var resp = await _network.PutUser(req);
            if (resp != null)
            {
                Debug.Log("PutRequest Succes " + resp);
                m_SignUpView.OnSignUpSuccess();
            }
            else
            {
                Debug.Log("PutRequest Fail " );
                m_SignUpView.OnSignUpError();
            }
            
        }

        private async void LoginRequest()
        {
            Debug.Log("Start LoginRequest = " );

            var resp = await _network.LoginUser(_loginReq);
            if (resp == null)
            {
                m_DebugLogText.text = "LoginResponse = null  ";
                m_LoginView.OnSLoginError();

            }
            else
            {
                m_DebugLogText.text = "Good  ";
                m_LoginView.OnLoginSuccess();
               
                LocalSettings.CurrentToken = resp.AccessToken;
                Debug.Log("LoginResponse = " + resp.Email + " "+ resp.AccessToken);
            }
        }
        
       
        
        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.L))
        //     {
        //         LoginRequest();
        //     }
        //     if (Input.GetKeyDown(KeyCode.P))
        //     {
        //         PutRequest();
        //     }
        //     if (Input.GetKeyDown(KeyCode.G))
        //     {
        //         GetRequest();
        //     }
        //     
        // }

        public void InitNewUserRequest(string mail, string password)
        {
            req.Email = mail;
            req.Password = password;
            // m_CurrentRegistrationHandler = registrationHandler;
            PutRequest();
        }
        
        public void InitLoginUserRequest(string mail, string password)
        {
            _loginReq.Email = mail;
            _loginReq.Password = password;
            // m_CurrentRegistrationHandler = registrationHandler;
            LoginRequest();
        }

        // public void StartLoginRequest()
        // {
        //     LoginRequest();
        // }
        
        // public void StartPutRequest()
        // {
        //     PutRequest();
        // }
    }
}