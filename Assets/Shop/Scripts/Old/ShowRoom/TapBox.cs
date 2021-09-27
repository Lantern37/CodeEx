using Shop.Core;
using UnityEngine;

namespace Shop
{
    
    public class TapBox : MonoBehaviour
    {
        private ShowRoom _showRoom;
        // void Update()
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         RaycastHit hit;
        //         Debug.Log("GetMouseButtonDown selected  " );
        //
        //         if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit))
        //         {
        //             Debug.Log("Physics selected  " );
        //
        //             var item = hit.transform.GetComponent<Item>();
        //             if (item != null)
        //             {
        //                 var lable = item.name;
        //                 _showRoom.BoxSelected(lable);
        //                 Debug.Log("Raycast selected  " + item.name);
        //
        //
        //             }
        //         }
        //     }
        //
        // }
    }

}