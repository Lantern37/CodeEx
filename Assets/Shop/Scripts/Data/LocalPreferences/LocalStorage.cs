using Engenious.Core.Managers;
using UnityEngine;

public class LocalStorage : MonoBehaviour
{
   private LoginUserResponce m_response;
   private string m_Token = "";

   public void SetNewToken(LoginUserResponce response)
   {
      m_Token = response.AccessToken;
      LocalSettings.CurrentToken = m_Token;

   }

   void GetCurrentToken()
   {
      m_Token = LocalSettings.CurrentToken;
   }
   

   public bool ExistToken()
   {
      GetCurrentToken();
      if (m_Token.Length > 0)
      {
         return true;
      }
      return false;
   }
}
