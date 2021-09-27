using System;
using DG.Tweening;
using UnityEngine;

public class BottomPanelControl : MonoBehaviour
{
    [SerializeField] private InputFieldControll[] m_InputFields;
    [SerializeField] private float m_UpToYPosition = 500f;
    private bool m_IsUp;
    private float m_StartYPosition;

    private void Awake()
    {
        m_StartYPosition = transform.position.y;
    }

    private void OnEnable()
    {
        foreach (var inputField in m_InputFields)
        {
            inputField.onFieldTouchEvent += PanelUp;
            inputField.onFieldEndEvent += PanelToStartY;
        }
    }

    private void OnDisable()
    {
        foreach (var inputField in m_InputFields)
        {
            inputField.onFieldTouchEvent -= PanelUp;
            inputField.onFieldEndEvent -= PanelToStartY;
        }
    }

    private void PanelUp()
    {
        // Debug.Log("PanelUp  ");

        if (m_IsUp) return;
            
        var yPosition = transform.position.y;
        yPosition += m_UpToYPosition;
        transform.DOMoveY(yPosition, 1);
        m_IsUp = true;
    }
    
    private void PanelToStartY()
    {
        Debug.Log("PanelToStartY  ");

        if (!m_IsUp) return;
            
        var yPosition = m_StartYPosition;
        transform.DOMoveY(yPosition, 1);
        m_IsUp = false;

    }

    public void WrongRegistaration()
    {
        foreach (var inputField in m_InputFields)
        {
            inputField.SetWrongState();
        }
    }
}
