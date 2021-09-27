using DG.Tweening;
using MySelectable;
using UnityEngine;
using Zenject;

namespace Shop.Core
{
    
    public class ItemInteractionSystem : MonoBehaviour, IUseInput
    {
        [Inject] InputManager m_Inputmanager;
        // [SerializeField] private TextMeshProUGUI _mainName;
        private ShowRoom _showRoom;


        private void OnEnable()
        {
            m_Inputmanager.onSelectedEvent += Interact;
        }

        private void OnDisable()
        {
            m_Inputmanager.onSelectedEvent -= Interact;
        }
        // private void Start()
        // {
        //     _showRoom = GetComponent<ShowRoom>();
        // }

        // private void Update()
        // {
        //     RaycastHit hit;
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        //         {
        //             Item item = hit.transform.GetComponent<Item>(); 
        //
        //             if (item != null)
        //             {
        //                 Interact(item);
        //             }
        //         }
        //     }
        // }
        public void Interact(Selectable item)
        {
            Debug.Log("Interact Item name " + item.name);
            var animator = item.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("Show");
                DOVirtual.DelayedCall(12, () => animator.Play("EndShow"));
            }
           
            
            // _showRoom.BoxSelected(item.name);
            AudioSystem.Instance.PlayOneShot(AudioClips.ItemSelect);

            // _mainName.text = item.name;
            // Debug.Log("Interaction with " + item.Name);
        }

        public void InitInput(InputManager input)
        {
            throw new System.NotImplementedException();
        }
    }
   
}