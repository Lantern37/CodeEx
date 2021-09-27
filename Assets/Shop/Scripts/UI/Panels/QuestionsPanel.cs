using Shop;
using TMPro;
using UnityEngine;

public class QuestionsPanel : MonoBehaviour
{
   [SerializeField] private GameObject _set_01;
   [SerializeField] private GameObject _dynamic;
   [SerializeField] private TextMeshProUGUI _dybanicQuestionText;

   public void Answer01()
   {
      AudioSystem.Instance.PlayOneShot(AudioClips.Hello);
   }
   
   public void Answer02()
   {
      AudioSystem.Instance.PlayOneShot(AudioClips.Hello);
   }

   public void SetDynamicQuestionButton(string question)
   {
      Debug.Log("SetDynamicQuestionButton");
      DynamicPanel();
      _dybanicQuestionText.text = question;
   }

   void DynamicPanel()
   {
      Debug.Log("DynamicPanel");

      if (_set_01.activeSelf)
      {
         _set_01.SetActive(false);
      }
      _dynamic.SetActive(true);
   }

   public void DynamicAnswer()
   {
      AudioSystem.Instance.PlayOneShot(AudioClips.Hello);
      _dynamic.SetActive(false);

   }
}
