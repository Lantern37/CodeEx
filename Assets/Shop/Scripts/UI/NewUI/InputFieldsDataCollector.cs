using UnityEngine;

public class InputFieldsDataCollector : MonoBehaviour
{
    [SerializeField] private InParentType m_ParentType;
    private string m_Password;
    private string m_Mail;
    
    
    public void  SetMail(string mail)
    {
        m_Mail = mail;
        Debug.Log("SetMail" );
    }
    
    public void  SetPassword(string password)
    {
        m_Password = password;
        Debug.Log("Set password" );
        SendData();
    }

    void SendData()
    {
        if (m_ParentType == InParentType.Login)
        {
            var login = GetComponent<LoginUITest>();
            login.SetDataFromInputs(m_Mail, m_Password);
        }
        else if (m_ParentType == InParentType.SignUp)
        {
            var login = GetComponent<SignUpUITest>();
            login.SetDataFromInputs(m_Mail, m_Password);
        }
    }
}

public enum InParentType
{
    SignUp, Login
}
