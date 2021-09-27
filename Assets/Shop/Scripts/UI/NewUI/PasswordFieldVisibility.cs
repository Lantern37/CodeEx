using UnityEngine;
using UnityEngine.UI;

public class PasswordFieldVisibility : MonoBehaviour
{
    [SerializeField] private Toggle m_Checkbox;
    [SerializeField] private InputField m_InputField;

    public void CheckboxToggled()
    {
        Debug.Log("Toggled");
        if (m_Checkbox.isOn)
        {
            Debug.Log("ON");

            m_InputField.contentType = InputField.ContentType.Password;
        }
        else
        {
            Debug.Log("OFF");

            m_InputField.contentType = InputField.ContentType.Standard;
        }
        m_InputField.ForceLabelUpdate();
        
    }
}
