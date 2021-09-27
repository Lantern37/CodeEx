using DG.Tweening;
using TMPro;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
   [SerializeField] private TMP_Text m_ItemName;
   [SerializeField] private Transform m_Bottom;
   [SerializeField] private float m_ItemPanelShowYPosition =  250f;
   [SerializeField] private float m_StartYPosition = -250f;
   public void SetName(string name)
   {
      m_ItemName.text = name;
   }
   public void  MoveUp(float duration)
   {
      // m_Bottom.DOMoveY(m_ItemPanelShowYPosition, duration);
   }
   public void  MoveDown(float duration)
   {
      // Debug.Log("Panel Down");
         // m_Bottom.DOMoveY(m_StartYPosition, duration);
   }
   

}
