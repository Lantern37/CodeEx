using System;
using UnityEngine;

public abstract class UiSelectObject<T> : MonoBehaviour
{
    [SerializeField] public GameObject LockImage;
    
    
    public abstract void SetListener(Action<T> action);
    
    public void SetLock(bool open)
    {
        LockImage.SetActive(!open);
    }

}
