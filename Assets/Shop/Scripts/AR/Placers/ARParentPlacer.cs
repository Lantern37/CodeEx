using System;
using Shop;
using Shop.Core;
using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;
using Zenject;

public class ARParentPlacer : MonoBehaviour
{
    [SerializeField] private PlacementReticle m_PlacementRecticle;
    [SerializeField] private InputManager m_InputManager;
    [SerializeField] private PlaneDetectionController m_PlaneDetectionController;

    [SerializeField] [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    private bool m_ARParentSpawned;
    private bool m_CanSpawn;

    public event Action SpawnParent;
    
    private void OnEnable()
    {
        m_InputManager.onTapEvent += SpawnARParent;
    }


    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }
    public GameObject spawnedObject { get; private set; }

    
    public void SpawnARParent()
    {

        if (m_ARParentSpawned || !m_CanSpawn)
        {
            return;
        }
        
        Debug.Log("Spawn Ar Parent TAP");
        Debug.Log("Spawn Ar Parent LableType " + ShopManager.Instance.LableType);
        if (ShopManager.Instance.LableType != LableType.None)
        {
            if (spawnedObject == null)
            {
                spawnedObject = 
                    Instantiate(m_PlacedPrefab, 
                        m_PlacementRecticle.GetReticlePosition().position, 
                        m_PlacementRecticle.GetReticlePosition().rotation);
                m_PlacementRecticle.HideRecticle();

                
                Debug.Log("OnTap AR Parent created -=" + ShopManager.Instance.LableType);
                m_InputManager.onTapEvent -= SpawnARParent;
                InitParentWithInput(m_InputManager, spawnedObject);
                
                SpawnParent?.Invoke();
            }

            m_ARParentSpawned = true;
            m_PlaneDetectionController.TogglePlaneDetection();
            
            //NewUI.Instance.OnSetState(3);
        }
           
    }

    public void SearchPlace()
    {
        m_CanSpawn = true;
        m_PlacementRecticle.ShowRecticle();
    }

    void InitParentWithInput(InputManager input, GameObject objToInit)
    {
        var useInput = objToInit.GetComponent<IUseInput>();
        if (useInput != null)
        {
            useInput.InitInput(input);
            
        }
    }

    public void Reset()
    {
        Debug.Log(" AR Parent reset");

        
        m_InputManager.onTapEvent += SpawnARParent;
        if (spawnedObject)
        {
            Destroy(spawnedObject);
        }
        m_ARParentSpawned = false;
        m_CanSpawn = false;
        m_PlacementRecticle.ShowRecticle();

        m_PlaneDetectionController.TogglePlaneDetection();
    }
}