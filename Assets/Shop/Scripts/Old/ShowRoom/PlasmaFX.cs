using DG.Tweening;
using UnityEngine;

public class PlasmaFX : MonoBehaviour
{
   [SerializeField] private GameObject _plasmaFX;

   private void Start()
   {
      _plasmaFX.SetActive(false);
   }

   public void Show()
   {
      _plasmaFX.SetActive(true);
      _plasmaFX.transform.localScale = Vector3.zero;
      _plasmaFX.transform.DOScale(new Vector3(1, 1, 1), 2);
   }
   
   public void Hide()
   {
      _plasmaFX.transform.DOScale(new Vector3(0, 0, 0), 1);
      DOVirtual.DelayedCall(1, ()=> _plasmaFX.SetActive(true));
   }
}
