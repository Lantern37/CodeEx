using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI.Extensions;

public class TestDropdown : MonoBehaviour
{
    [SerializeField] private AutoCompleteComboBox _input;

    [SerializeField] private List<string> _items;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            List<string> list = new List<string>();

            foreach (var item in _items)
            {
                list.Add(item);
            }
            
            _input.SetAvailableOptions(list);
        }
    }
}
