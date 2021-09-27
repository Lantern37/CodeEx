using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Utilities
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class OpenHyperlinks : MonoBehaviour, IPointerClickHandler
    {
        private TextMeshProUGUI _linkText;
        [SerializeField] private Camera _uiCamera;

        private void Awake()
        {
            _linkText = GetComponent<TextMeshProUGUI>();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Vector2 pos = new Vector2();

#if UNITY_EDITOR
            pos = Input.mousePosition;
#else
            pos = Input.GetTouch(0).position;            
#endif

            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_linkText, pos, _uiCamera);
            if( linkIndex != -1 ) 
            { // was a link clicked?
                TMP_LinkInfo linkInfo = _linkText.textInfo.linkInfo[linkIndex];

                // open the link id as a url, which is the metadata we added in the text field
                Application.OpenURL(linkInfo.GetLinkID());
            }
        }
    }
}