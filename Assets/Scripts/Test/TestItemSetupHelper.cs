using System.Collections;
using System.Collections.Generic;
using Shop.Core;
using UnityEngine;

public class TestItemSetupHelper : MonoBehaviour
{
    [SerializeField] private string _cutCompanyName;

    [SerializeField] private string _cutEnd;

    [SerializeField] private string _replace;

    [Space(10)]
    [SerializeField] private string _transformName;

    [SerializeField] private List<Item> _items;
    
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //GiveNames();
            FindTransform();
        }
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            ShowAnim();
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            CloseAnim();
        }
        
        
    }

    private void GiveNames()
    {
        string txt = "";
        foreach (var item in _items)
        {
            string name = item.gameObject.name.Replace(_cutCompanyName , "");
            name = RemoveFromEnd(name, _cutEnd);
            name = name.Replace(_replace, "");

            item.name = name;
            txt += name + "\n";
        }

        Debug.Log(txt);
    }
    
    public string RemoveFromEnd(string s, string suffix)
    {
        if (s.EndsWith(suffix))
        {
            return s.Substring(0, s.Length - suffix.Length);
        }
        else
        {
            return s;
        }
    }

    private void ShowAnim()
    {
        foreach (Item item in _items)
        {
            item.Anim.Play("Show");
        }
    }

    private void CloseAnim()
    {
        foreach (Item item in _items)
        {
            item.Anim.Play("EndShow");
        }
    }

    private void FindTransform()
    {
        foreach (Item item in _items)
        {
            SetItemTransform(item, _transformName);
        }
    }
    
    private void SetItemTransform(Item item, string name)
    {
        Transform tr = item.transform.Find(name);
        item.ProductTransform = tr;
    }
}
