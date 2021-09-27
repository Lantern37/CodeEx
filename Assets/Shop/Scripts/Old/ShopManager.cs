using Shop.Core.Utils;
using UnityEngine;


namespace Shop.Core
{
    
    public class ShopManager : SingletonT<ShopManager>
    {
        [SerializeField] private NewUI m_NewUI;
        [SerializeField] private DebugUIPathController m_DebugUIPathController;
        private PathBehaviour m_PathBehaviour;
        private Path m_Path;
        private bool m_InitDone;
        public LableType LableType
        {
            get;
            set;
        }

        private void Start()
        {
            LableType = LableType.None;
            
        }
        public void Init(PathBehaviour path)
        {
            m_PathBehaviour = path;
        }
        
        public void Init(Path path)
        {
            m_Path = path;
        }

        void LoadLable(LableType type)
        {
            // m_PathBehaviour.MainCollection;
        }

        
        //Debug Panel 
        #region PathControl

        public void MovePathForward()
        {
            if (m_Path != null)
            {
                // Debug.Log("Pointer from");
                m_Path.MovePathForward();
            } 
        } 
        public void MovePathBackward()
        {

            if (m_Path != null)
            {
                // Debug.Log("Pointer To");
                m_Path.MovePathBackward();
            } 
        } 
        public void MovePathStop()
        {

            if (m_Path != null)
            {
                // Debug.Log("Pointer Up");
                m_Path.MovePathStop();
            } 
        }

        public void MovePathLengthX(bool increment)
        {
            if (m_Path != null)
            {
                // Debug.Log("Pointer Up");
                m_Path.MovePointX(increment);
            } 
        }

        //  public void MovePathEndPointX(bool increment)
        // {
        //     if (m_Path != null)
        //     {
        //         // Debug.Log("Pointer Up");
        //         m_Path.MovePointX(increment);
        //     } 
        // }
        
        public void MovePathMiddlePointZ(bool increment)
        {
            if (m_Path != null)
            {
                // Debug.Log("Pointer Up");
                m_Path.MoveMiddleZ(increment);
            } 
        }

        public void ChangeDistance(bool increment)
        {
            if (m_Path != null)
            {
                // Debug.Log("Pointer Up");
                m_Path.ChangeDistanseBetweenItems(increment);
            } 
        }
         public void StopMovePathPoint()
         {
             if (m_Path != null)
             {
                 // Debug.Log("Pointer Up");
                 m_Path.StopMovePoint();
             } 
         }

         
         
         //Text
         // public void UpdateTextStartX(string value)
         // {
         //     m_DebugUIPathController.SetStartXText(value);
         // }
         
         // public void UpdateTextEndX(string value)
         // {
         //     m_DebugUIPathController.SetEndXText(value);
         // }
         
         public void UpdateLength(string value)
         {
             m_DebugUIPathController.SetPathLengthText(value);
         }
         
         public void UpdateTextSplineZ(string value)
         {
             m_DebugUIPathController.SetSplineZText(value);
         }
         
         public void UpdateTextMiddleZ(string value)
         {
             m_DebugUIPathController.SetMiddleZText(value);
         }
         
         public void UpdateTextDistance(string value)
         {
             m_DebugUIPathController.SetDistanceText(value);
         }

        
        #endregion

        #region Selected Item
        
        public void SelectedItem(ISelectable selectable)
        {
            var go = selectable as MonoBehaviour;
            var item = go.GetComponentInChildren<Item>();
            m_NewUI.OnShowItemPanel(item);
            Debug.Log("SHOP Manager got selected Item name " + item.name);
        }

        public void UnselectItem()
        {
            m_NewUI.OnHideItemPanel();
            // Debug.Log("SHOP Manager unselect " );
        }

        public void ItemInteracted()
        {
            SetQuestion("Tell me more about this");
        }
        

        #endregion
        
        #region Depricated
        void SetQuestion(string question)
        {
            UI.Instance.SetQuestion(question);
        }

        public void SetLable(int lable)
        {
            LableType = (LableType)lable;
            
            var spawnControl = SpawnerControll.Instance;
            if (spawnControl != null)
            {
                spawnControl.OnLableChoosen();
            }
        }

        #endregion

    }

    public enum LableType
    {
        None,
        Cure,
        Bloom,
        Plug,
        Almora,
        Camino,
        CRU,
        PacificStone,
        PureVape,
        Roveremedies,
        Stiiizy
    }
   
}