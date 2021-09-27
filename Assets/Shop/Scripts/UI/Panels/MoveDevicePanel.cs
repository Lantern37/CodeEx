using DG.Tweening;
using Shop.Behaviours;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class MoveDevicePanel : MonoBehaviour
{
    [SerializeField] private GameObject _phoneAnimation;
    [SerializeField] private GameObject _planeAnimation;
    
    private CanvasGroup _canvasGroup;
    public ScanSteps ScanStep { get; private set; }

    // [Inject] private ARCamera _arCamera;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.alpha = 0f;
        
        // _arCamera.CameraDirectionChanged += CameraDirectionChanged;
    }

    // private void CameraDirectionChanged(CameraDirections cameraDirection)
    // {
    //     switch (cameraDirection)
    //     {
    //         case CameraDirections.Forward:
    //             _phoneAnimation.SetActive(false);
    //             _planeAnimation.SetActive(true);
    //             break;
    //         case CameraDirections.Down:
    //             _phoneAnimation.SetActive(true);
    //             _planeAnimation.SetActive(false);
    //             break;
    //     }
    // }

    
    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1f, 0.4f);
    }

    public void Hide()
    {
        _canvasGroup.DOFade(0f, 0.6f); 
        gameObject.SetActive(false);
    }
    
    public void ShowFindPlaceStep()
    {
        ScanStep = ScanSteps.FindPlace;
        
        // _phoneAnimation.Show();

        // _messageTmp.text = LocalizationManager.Instance.GetLocalizationValue("scanning_find");
        
        // _distancePanel.ShowSearchingHelp();
    }

    public void ShowTapToPlaceStep()
    {
        ScanStep = ScanSteps.TapToPlace;
        
        // _phoneAnimation.Hide();

        // _messageTmp.text = LocalizationManager.Instance.GetLocalizationValue("scanning_tap");
        
        // _distancePanel.StartDistanceHelping();
    }

    
}



public enum ScanSteps
{
    FindPlace,
    TapToPlace
}
