using UnityEngine;

public class TriggerRenderSwitcher : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      ItemPathParent item = other.gameObject.GetComponent<ItemPathParent>();
      if (item != null)
      {
         item.SetRenderActive(false);
         Debug.Log("OnTriggerEnter Delta " + item.name);

      }
   }
   
   private void OnTriggerExit(Collider other)
   {
      ItemPathParent item = other.gameObject.GetComponent<ItemPathParent>();
      if (item != null)
      {
         item.SetRenderActive(true);
         Debug.Log("OnTriggerEnter Delta " + item.name);

      }
   }
}
