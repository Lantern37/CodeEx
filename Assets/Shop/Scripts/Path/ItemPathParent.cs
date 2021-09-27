using DG.Tweening;
using Dreamteck.Splines;
using Engenious.Core;
using Shop.Core;
using UnityEngine;

public class ItemPathParent : MonoBehaviour, ISelectable
{
    [SerializeField] private bool _isMove;
    
    private SplineComputer _spline;
    private SplinePositioner _positioner;
    private Vector3 m_Normalscale = Vector3.one;
    public SplinePositioner Positioner => _positioner;
    
    private bool _isLocalHead = false;
    public bool IsLocalHead  
    {
        get => _isLocalHead;
        set => _isLocalHead = value;
    }

    private float _splinePosition = 0;
    public float SplinePosition
    {
        get => _splinePosition;
        set => _splinePosition = value;
    }

    private bool _isSetupDone = false;
    private bool _isRenderOn = false;
    
    private int _pathIndex = -1;
    public int PathIndex
    {
        get => _pathIndex;
        set => _pathIndex = value;
    }

    
    private bool m_IsSelected;
    public bool IsSelected => m_IsSelected;
    

    private PathBehaviour _splineBehaviour;
    private bool isInitDone = false;
    // private float m_DistanceToNext;
    
    public void ConnectToSpline(SplineComputer spline, float yOffset)
    {
        _positioner = gameObject.AddComponent<SplinePositioner>();
        _positioner.spline = spline;
        _positioner.mode = SplinePositioner.Mode.Distance;
        _positioner.useTriggers = true;
        _positioner.motion.offset = new Vector2(0, yOffset);
        _positioner.motion.offset = new Vector2(0, yOffset);
        _positioner.motion.rotationOffset = new Vector3(0, 90, 0);
        _positioner.SetDistance(0);
    }
    
    public void InitSplineBehaviour(PathBehaviour behaviour)
    {
        _splineBehaviour = behaviour;
        // m_DistanceToNext = _splineBehaviour.DistanceBetweenItems;
        isInitDone = true;
    }
    
    
    //Update
    public void Tick()
    {
        if (!isInitDone) return;
       
        if (!_isLocalHead && _pathIndex > 0 && _pathIndex < _splineBehaviour.CurrentLable.Count)
        {
            var frontBallPosition = (float)_splineBehaviour.CurrentLable[_pathIndex - 1].Positioner.position;
            var toPosition = frontBallPosition - _splineBehaviour.DistanceBetweenItems;
            DOTween.To(
                ()=> _splinePosition, 
                x=> _splinePosition = x, 
                toPosition, 
                0f
                );
            // _splinePosition = toPosition; //????
          
            
            _positioner.SetDistance(_splinePosition, true);
        }


        // if (_isRenderOn)
        // {
        //     if (_splinePosition < _splineBehaviour.DistanceBetweenItems || _splinePosition > _splineBehaviour.SplineLength - _splineBehaviour.DistanceBetweenItems )
        //     {
        //         SetRenderActive(false);
        //         _isRenderOn = false;
        //     }
        // }

        // if (!_isRenderOn)
        // {
        //     if (_splinePosition > _splineBehaviour.DistanceBetweenItems)
        //     {
        //         SetRenderActive(true);
        //         _isRenderOn = true;
        //     }
        // }
    }
     
     public void SetRenderActive(bool isActive)
     {
         GetComponentInChildren<Item>().SetRenderer(isActive);
         // Debug.Log("Renderer in " + transform.name + " is " + isActive + " Position " + _splinePosition);
     }
     
     public void SwitchRender()
     {
         GetComponentInChildren<Item>().SwitchRenderer();
         // Debug.Log("Renderer in " + transform.name + " is " + isActive + " Position " + _splinePosition);
     }

     //Interface ISelectable
     public void SelectedByPoint()
     {
         Debug.Log(" Parent  SelectedByPoint " + this.name);
     }

     public void SelectedByScreenRay()
     {
         // Debug.Log("  SelectedByScreenRay " + this.name);
         var scale = transform.localScale;
         var newScale = new Vector3(scale.x * 1.17f,scale.y * 1.17f,scale.z * 1.17f);
         // transform.DOScale(newScale, 0.7f).SetEase(Ease.OutBounce);
     }

     public void UnSelectedByScreenRay()
     {
         // Debug.Log("  UnSelectedByScreenRay " + this.name);
         transform.DOScale(m_Normalscale, 0.5f).SetEase(Ease.OutBounce);
     }
     
     public void SelectedByCentralTrigger()
     {
         // Debug.Log("  SelectedByScreenRay " + this.name);
         var scale = transform.localScale;
         var newScale = new Vector3(scale.x * 1.17f,scale.y * 1.17f,scale.z * 1.17f);
         transform.DOScale(newScale, 0.7f).SetEase(Ease.OutBounce);
     }

     public void UnSelectedByCentralTrigger()
     {
         // Debug.Log("  UnSelectedByScreenRay " + this.name);
         transform.DOScale(m_Normalscale, 0.5f).SetEase(Ease.OutBounce);
     }
     
}
