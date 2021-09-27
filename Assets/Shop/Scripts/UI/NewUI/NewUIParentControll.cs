using UnityEngine;

public class NewUIParentControll : MonoBehaviour
{
   [SerializeField] private GameObject[] m_Panels;
   private NewUI m_NewUI;
   
   private int m_CurrentIndex;

   public void Init(NewUI ui)
   {
      Debug.Log("Init  " + this.name);

      m_NewUI = ui;
      m_CurrentIndex = 0;
      HideAll();

      StartControl();
   }

   private void StartControl()
   {
      ShowPanel(m_CurrentIndex);
      // m_CurrentIndex++;
   }
   
   public void ShowPanel(int index)
   {
      HideAll();
      m_Panels[index].SetActive(true);
   }

   private void HideAll()
   {
      foreach (var panel in m_Panels)
      {
         panel.SetActive(false);
      }

   }

   public void OnClickNext()
   {

      m_CurrentIndex++;   
      Debug.Log("OnClickNext current index " + m_CurrentIndex);

      if (m_CurrentIndex >= m_Panels.Length)
      {
         m_CurrentIndex = m_Panels.Length-1;
      }
     
      ShowPanel(m_CurrentIndex);
      
   }
   
   public void OnClickBack()
   {
      Debug.Log("current index " + m_CurrentIndex);
      
         m_CurrentIndex--;
         if (m_CurrentIndex < 0)
         {
            m_CurrentIndex = 0;
         }
         ShowPanel(m_CurrentIndex);
       }
}
