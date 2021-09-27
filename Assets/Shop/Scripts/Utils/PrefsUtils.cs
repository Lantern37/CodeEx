using System;
using Engenious.Core.Managers;
using UnityEngine;

public static class PrefsUtils
{

    public static string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }
    
    public static void SetString(string key, string defaultValue)
    {
        PlayerPrefs.SetString(key, defaultValue);
    }
    
   
    
}


