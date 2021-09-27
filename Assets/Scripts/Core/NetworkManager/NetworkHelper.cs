using System;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

public class NetworkHelper
{
    public static string GenerateHMAC(string apiKey, string deviceID, string nonce)
    {
        var сoncatenation = $"{deviceID}:{nonce}";
        var encoding = new ASCIIEncoding();
        HMACSHA256 hmac = new HMACSHA256(encoding.GetBytes(apiKey));
        return HashEncode(hmac.ComputeHash(encoding.GetBytes(сoncatenation)));
    }

    private static string HashEncode(byte[] hash)
    {
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}

