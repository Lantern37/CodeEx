using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCenter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemPathParent item = other.gameObject.GetComponent<ItemPathParent>();
        if (item != null)
        {
            item.SelectedByCentralTrigger();
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        ItemPathParent item = other.gameObject.GetComponent<ItemPathParent>();
        if (item != null)
        {
            item.UnSelectedByCentralTrigger();
        }
    }
}
