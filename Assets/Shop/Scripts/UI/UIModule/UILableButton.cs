using System;
using Shop.Core;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class UILableButton : UiSelectObject<LableType>
{
    [SerializeField] private LableType m_LableType;
    public override void SetListener(Action<LableType> action)
    {
        GetComponent<Button>().onClick.AddListener(()=> action(m_LableType));
    }
}
