using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class InputFieldControll : MonoBehaviour
{
    [SerializeField] private InputField m_InputField;
    [SerializeField] private InputFieldsDataCollector m_DataCollector;
    [SerializeField] private IputFieldType m_InputType;
    [SerializeField] private Image m_Image;
    [SerializeField] private Sprite m_NormalSprite;
    [SerializeField] private Sprite m_WrongSprite;

    
    public  const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

    public const string MatchPasswordPattern =
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";
    
    private void Start()
    {
        SetNormalState();
        m_InputField.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
        // m_InputField.OnPointerClick().AddListener(delegate {ValueChangeCheck(); });
    }
    
    public static bool ValidateEmail (string email)
    {
        if (email != null)
            return Regex.IsMatch (email, MatchEmailPattern);
        else
        {
            return false;
        }
    }
    
    public static bool ValidatePassword (string password)
    {
        if (password != null)
            return Regex.IsMatch (password, MatchPasswordPattern);
        else
            return false;
    }

    public void SetWrongState()
    {
        m_Image.sprite = m_WrongSprite;
    }
    
    public void SetNormalState()
    {
        m_Image.sprite = m_NormalSprite;
    }
    
  
// Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        // Debug.Log("Value Changed ValueChangeCheck");
    }
    public void OnValueChanged(string str)
    {
        // Debug.Log("OnValueChanged " + str);
    }
    
    public void OnSubmitString(string str)
    {
        // Debug.Log("OnSubmitString " + str);
    }
    
    public void OnEnd(string str)
    {
        Debug.Log("OnEnd " + m_InputField.text);
        if (m_InputType == IputFieldType.Email)
        {
            if (ValidateEmail(m_InputField.text))
            {
                Debug.Log("Your   Mail is " + m_InputField.text);
                m_DataCollector.SetMail(m_InputField.text);
            }
            else
            {
                Debug.Log("Your Mail is wrong" );
                m_InputField.ActivateInputField();
                SetWrongState();
                return;
            }
        }
        else  if (m_InputType == IputFieldType.Password)
        {
            // if (ValidatePassword(m_InputField.text))
            // {
            //     Debug.Log("Your   Pass is " + m_InputField.text);
            //     m_LoginUi.SetPassword(m_InputField.text);
            // }
            // else
            // {
            //     Debug.Log("Your Password do not mach" );
            //     m_InputField.ActivateInputField();
            //     return;
            // }
            
            m_DataCollector.SetPassword(m_InputField.text);

        }
        
        
        OnEndEvent();
      
    }

    public void OnFieldClick()
    {
        // Debug.Log("OnFieldClick  ");
        OnStartEvent();
    }
    
    
    //Envocation
    private void OnStartEvent()
    {
        m_OnFieldTouchEvent?.Invoke();
        SetNormalState();
    }
    
    private void OnEndEvent()
    {
        Debug.Log("OnFieldClick  m_OnFieldEndEvent?.Invoke(); ");

        m_OnFieldEndEvent?.Invoke();
    }



    #region Events

    //Event Start touch
    private  event Action m_OnFieldTouchEvent;
    public event Action  onFieldTouchEvent
    {
        add
        {
            if (m_OnFieldTouchEvent == null || !m_OnFieldTouchEvent.GetInvocationList().Contains(value))
            {
                m_OnFieldTouchEvent += value;
            }
        }

        remove
        {
            if (m_OnFieldTouchEvent.GetInvocationList().Contains(value))
            {
                m_OnFieldTouchEvent -= value;
            }
        }
    }
    
    //Event End touch
    private  event Action m_OnFieldEndEvent;
    public event Action  onFieldEndEvent
    {
        add
        {
            if (m_OnFieldEndEvent == null || !m_OnFieldEndEvent.GetInvocationList().Contains(value))
            {
                m_OnFieldEndEvent += value;
            }
        }

        remove
        {
            if (m_OnFieldEndEvent.GetInvocationList().Contains(value))
            {
                m_OnFieldEndEvent -= value;
            }
        }
    }

    #endregion
 
   
}

public enum IputFieldType
{
    Email, Password, PhoneNumber
}
