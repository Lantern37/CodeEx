using System;
using Shop.Behaviours.AR;
using Shop.Core.Utils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace Shop
{
    
public class SpawnManager : SingletonT<SpawnManager> 
{
    public event Action ShopSpawned;
    
    // [SerializeField] private InputManager m_InputManager;
    [SerializeField] private ARPlaneManager _planeManager;
    [SerializeField] private ARCameraManager _cameraManager;
    [SerializeField] private ARParentPlacer m_ArParentPlacer;
    [SerializeField] private ProductPresenter m_Presenter;
    private PlayerPlacer _playerPlacer;
    private PreviewPlacer _previewPlacer;
    private UI _ui;
    private bool _isNeedSpawn;
    private bool _isPlayerPlaced;
    
    
    // [Inject]
    // private void Construct(ARPlaneManager planeManager
    //                        // ARCameraManager cameraManager
    //                        // PlayerPlacer playerPlacer,
    //                        // PreviewPlacer previewPlacer 
    //                        // UIManager uiManager
    //                        )
    // {
    //     _planeManager = planeManager;
    //     // _cameraManager = cameraManager;
    //     // _playerPlacer = playerPlacer;
    //     // _previewPlacer = previewPlacer;
    //     // _uiManager = uiManager;
    //     
    //     // Debug.Log(
    //     //     "_planeManager  " 
    //     // + _planeManager.name + " \n   _cameraManager "
    //     //     + _cameraManager.name  + " \n  _playerPlacer  "
    //     //     + _playerPlacer.name  + " \n _previewPlacer "
    //     //     + _previewPlacer.name  + " \n _uiManager  "
    //     //     + _uiManager.name  + " \n"
    //     //     );
    //     
    //     Debug.Log(" _planeManager " + _planeManager.name);
    //     Debug.Log(" _cameraManager " + _cameraManager.name);
    //     // Debug.Log(" _playerPlacer " + _playerPlacer.name);
    //     // Debug.Log(" _previewPlacer " + _previewPlacer.name);
    //
    // }

    private void Awake()
    {
        _ui = UI.Instance;
        Debug.Log(" _uiManager " + _ui.name);
    }

    private void OnEnable()
    {
        _cameraManager.frameReceived += FrameChanged;
        // m_InputManager.onTapEvent += () => 

    }

    private void OnDisable()
    {
        _cameraManager.frameReceived -= FrameChanged;
    }


    public void StartSearchigPlace()
    {
        Debug.Log("StartSearchigPlace  Spawn in manager");

        m_ArParentPlacer.SearchPlace();
        _isNeedSpawn = true;
    }
    
    public void EndSpawning()
    {
        _isNeedSpawn = false;
        ShopSpawned?.Invoke();
        Debug.Log("End Spawn in manager");
    }
    
    
    public void Reset()
    {
        Debug.Log("spawn reset");
        _isPlayerPlaced = false;

        if (_planeManager == null)
        {
            _planeManager = FindObjectOfType<ARPlaneManager>();
        }
        _planeManager.subsystem?.Start();
        
        foreach (var trackable in _planeManager.trackables)
        {
            trackable.gameObject.SetActive(true);
        }
        SetPlanesEnabled(true);
        m_Presenter.ResetPresenter();

        // StartSpawning();
    }
    
    
    // public void Update()
    // {
    //     if (!_isNeedSpawn || _isPlayerPlaced)
    //     {
    //         return;
    //     }
    //     
    //     // TrySpawn();
    // }
    //
    private void TrySpawn()
    {
        Pose hit;
        if (!_playerPlacer.TryGetHit(out  hit))
        {
            _previewPlacer.Hide();
            return;
        }

        // _uiManager.HideMoveDevicePanel();
        _previewPlacer.Move(hit.position);
        
        
        
        if (!_playerPlacer.IsTouch())
        {
            return;
        }
            
        _isPlayerPlaced = true;
        _previewPlacer.Hide();
            
        _playerPlacer.SpawnPlayer(hit);

        PlayerSpawned();
        SetPlanesEnabled(false);
    }
    
  
    private void PlayerSpawned()
    {
        // _uiManager.PlayerSpawned();
        _planeManager.subsystem?.Stop();
        foreach (var trackable in _planeManager.trackables)
        {
            trackable.gameObject.SetActive(false);
        }
        
        EndSpawning();
    }


    private void FrameChanged(ARCameraFrameEventArgs args)
    {
        if (!PlaneDetected())
        {
            return;
        }
            
        _cameraManager.frameReceived -= FrameChanged;
    }

    private bool PlaneDetected()
    {
        return _planeManager.trackables.count > 0;
    }
    
    
     
    private void SetPlanesEnabled(bool isEnabled)
    {
        if (isEnabled)
        {
            _planeManager.subsystem?.Start();
        }
        else
        {
            _planeManager.subsystem?.Stop();
        }
        
        foreach (var trackable in _planeManager.trackables)
        {
            trackable.gameObject.SetActive(isEnabled);
        }
    }
    
}   
}