using System;
using Engenious.Core.Managers;
using UnityEngine;

public static class LocalSettings
{
    private const string TokenKey = "Token";
    

    public static string CurrentToken
    {
        get => PrefsUtils.GetString(TokenKey);
        set => PrefsUtils.SetString(TokenKey, value);
    }
    
  
   
}
