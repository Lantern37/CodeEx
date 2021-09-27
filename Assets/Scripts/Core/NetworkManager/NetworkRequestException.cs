using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkRequestException : Exception
{
    public string Code { get; private set; }

    public NetworkRequestException(string code, string message)
        : base(message)
    {
        Code = code;
        Debug.Log("<color=#ff0000> NetworkRequestException code " + code + " message " + message +"</color>");
    }
}
