using System;
using System.Linq;
using UnityEngine;

public class UIControlBase <T,R> : MonoBehaviour where R : UiSelectObject<T>
{
    protected R[] m_UIChoiceOptions;

    protected virtual void Awake()
    {
        m_UIChoiceOptions = GetComponentsInChildren<R>();

        foreach (var option in m_UIChoiceOptions)
        {
            option.SetListener(SetOptionData);
        }
    }

    public void SetOptionData(T optionData)
    {
        OnSetOptionEvent(optionData);
    }


    //Event
    private event Action<T> m_OnSetOptionEvent;
    public event Action<T> onSetOptionEvent
    {
        add
        {
            if (m_OnSetOptionEvent == null || !m_OnSetOptionEvent.GetInvocationList().Contains(value))
            {
                m_OnSetOptionEvent += value;
            }
        }

        remove
        {
            if (m_OnSetOptionEvent.GetInvocationList().Contains(value))
            {
                m_OnSetOptionEvent -= value;
            }
        }
    }
    private void OnSetOptionEvent(T option)
    {
        if (m_OnSetOptionEvent != null)
        {
            m_OnSetOptionEvent(option);
        }
    }
}
