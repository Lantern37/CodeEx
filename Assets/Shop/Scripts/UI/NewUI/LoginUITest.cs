using System;
using Assets.Scripts.Test;
using UnityEngine;
using UnityEngine.UI;

public class LoginUITest : MonoBehaviour, IRegistrationHandler, ILoginUPView
{
    [SerializeField] private TestNetwork m_Network;
    [SerializeField] private Button m_button;
    [SerializeField] private NewUI m_NewUI;

    private string m_Mail;
    private string m_Password;

    private bool m_RequestSent;

    private void OnEnable()
    {
        m_RequestSent = false;
        SetButtonActive(false);
    }

    // private void Start()
    // {
    //     SetButtonActive(false);
    // }

    public void SendLoginRequest()
    {
        if (m_RequestSent)
        {
            return;
        }
        Debug.Log("SendLoginRequest by click" );

        m_Network.InitLoginUserRequest(m_Mail, m_Password);
        // m_Network.StartLoginRequest();
        m_RequestSent = true;
    }

    public void  SetMail(string mail)
    {
        m_Mail = mail;
        Debug.Log("SetMail" );
    }
    
    public void  SetPassword(string password)
    {
        m_Password = password;
        Debug.Log("Set password" );
        SetButtonActive(true);
    }

    public void SetDataFromInputs(string mail, string password)
    {
        m_Mail = mail;
        m_Password = password;
        SetButtonActive(true);

    }

    public void BadRegistrationHandle()
    {
        Debug.Log("Given email or password was invalid. Please try again" );
        SetButtonActive(false);
        m_RequestSent = false;
    }

    public void SetButtonActive(bool active)
    {
        m_button.interactable = active;
    }

    public void OnLoginSuccess()
    {
        m_NewUI.OnSetState(8);
    }

    public void OnSLoginError()
    {
        Debug.Log("LoginResponse = null  ");
        BadRegistrationHandle();
    }
}


public interface ILoginUPView
{
    public void OnLoginSuccess();
    public void OnSLoginError();
}