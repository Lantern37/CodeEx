using Assets.Scripts.Test;
using UnityEngine;
using UnityEngine.UI;

public class SignUpUITest : MonoBehaviour, IRegistrationHandler, ISighnUPView
{
    [SerializeField] private TestNetwork m_Network;
    [SerializeField] private Button m_button;
    [SerializeField] private NewUI m_NewUI;
    private string m_Mail;
    private string m_Password;

    private bool m_RequestSent;

    private void Start()
    {
        SetButtonActive(false);
        m_Network.SignUpView = this;
    }

    public void SendNewUserRequest()
    {
        if (m_RequestSent)
        {
            return;
        }
        Debug.Log("SendLoginRequest by click" );

        m_Network.InitNewUserRequest(m_Mail, m_Password);
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

    }

    public void SetButtonActive(bool active)
    {
        m_button.interactable = active;
    }


    public void OnSignUpSuccess()
    {
        m_RequestSent = false;
        m_NewUI.OnSetState(0);
    }

    public void OnSignUpError()
    {
        BadRegistrationHandle();
        m_RequestSent = false;
    }
}


public interface ISighnUPView
{
    public void OnSignUpSuccess();
    public void OnSignUpError();
}