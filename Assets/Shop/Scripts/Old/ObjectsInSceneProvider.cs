 using System.Collections.Generic;
 using System.Linq;
 using DG.Tweening;
 using Shop;
using Shop.Core;
using UnityEngine;

public class ObjectsInSceneProvider : MonoBehaviour, IUseInput
{
   [SerializeField] private GameObject m_PathPrefab;
   [SerializeField] private GameObject m_PathItemPrefab;
   [SerializeField] private GameObject m_CurePrefab;
   [SerializeField] private GameObject m_BloomPrefab;
   [SerializeField] private GameObject m_PlugPrefab;
   [SerializeField] private GameObject m_AlmoraPrefab;
   [SerializeField] private GameObject m_CaminoPrefab;
   [SerializeField] private GameObject m_CRUPrefab;
   [SerializeField] private GameObject m_PacificStonePrefab;
   [SerializeField] private GameObject m_PureVapePrefab;
   [SerializeField] private GameObject m_RoveremediesPrefab;
   [SerializeField] private GameObject m_StiiizyPrefab;
   private InputManager m_InputManager;
   private Transform m_ArParent;
   private ShopManager m_ShopManager;
   private Path m_Path;
   
   public Transform ArParent
   {
      get { return m_ArParent;}
      private set { m_ArParent = value;}
   }
   
   
   public GameObject spawnedLable { get; private set; }

   public List<Item> GetItems()
   {
      Item[] items = null;

      if (m_Path != null)
      {
         items = m_Path.GetComponentsInChildren<Item>();
         Debug.LogError("SPAWNABLE " + items.Length);
      }

      return items.ToList();
   }

   private void Start()
   {
      m_ShopManager = ShopManager.Instance;
      ArParent = transform;
      SpawnPath();
      DOVirtual.DelayedCall(2, () => SpawnLable(m_ShopManager.LableType));

   }

   void SpawnPath()
   {
      m_Path = Instantiate(m_PathPrefab, transform).GetComponent<Path>();
      m_Path.Init();
   }

   public void SpawnLable(LableType lableType)
   {
      GameObject lablePrefab = m_CurePrefab;
      switch(lableType)
      {
         case LableType.Cure:
            lablePrefab = m_CurePrefab;
            break;
         
         case LableType.Bloom:
            lablePrefab = m_BloomPrefab;
            break;
         
         case LableType.Plug:
            lablePrefab = m_PlugPrefab;
            break;
          
         case LableType.Camino:
            lablePrefab = m_CaminoPrefab;
            break;
         
         case LableType.CRU:
            lablePrefab = m_CRUPrefab;
            break;
         
         case LableType.PacificStone:
            lablePrefab = m_PacificStonePrefab;
            break;
         
         case LableType.PureVape:
            lablePrefab = m_PureVapePrefab;
            break;
         
         case LableType.Roveremedies:
            lablePrefab = m_RoveremediesPrefab;
            break;
         
         case LableType.Almora:
            lablePrefab = m_AlmoraPrefab;
            break;
         
         case LableType.Stiiizy:
            lablePrefab = m_StiiizyPrefab;
            break;
         
      }
      spawnedLable = Instantiate(lablePrefab, ArParent);
      LoadNewLableToPath(spawnedLable);
      UI.Instance.ARParenSpawned();
      
   }

   void LoadNewLableToPath(GameObject lable)
   {
      var lableList = new List<ItemPathParent>();
      var lableContainer = lable.GetComponentsInChildren<Item>();
      foreach (Item item in lableContainer)
      {
         var itemParent = Instantiate(m_PathItemPrefab, m_Path.transform);
         item.transform.SetParent(itemParent.transform);
         ItemPathParent itemPathParent = itemParent.GetComponent<ItemPathParent>();
         lableList.Add(itemPathParent);
         itemPathParent.SetRenderActive(false);
         // Debug.Log("Item added");
      }

      m_Path.SetNewLable(lableList);
      Destroy(lable);
   }
   
   public void InitInput(InputManager input) => m_InputManager = input;
}
