using System;
using Assets.Scripts.Utilities;
using DG.Tweening;
using Shop.Core;
using UnityEngine;
using Zenject;

public class ProductPresenter : MonoBehaviour, IUseInput
{
    // [SerializeField] private ItemSelector m_ItemSelector;
    [SerializeField] private ItemRotatorConfig _rotatorConfig;

    [SerializeField] private float _productShowHeight = 0.5f;
    
    [SerializeField] private Transform m_PresentPlace;
    private Transform m_ItemParentTransform;
    private Vector3 m_ItemStartPosition;
    private Vector3 m_ItemStartRotationEuler;
    private bool m_Action = false;

    private Vector3 _beforeShowPosition;

    private ItemRotator _itemRotator;
    private Item _selectedItem;  
    
    [SerializeField] private InputManager m_InputManager;
    [SerializeField] private RaycastSystem m_RaycastSystem;

    [SerializeField] private TestSwipeDetection _swipe;

    public event Action<Item> StartPresentation;
    public event Action<Item> EndPresentation;
    public event Action<Item> StartOpenProduct;
    public event Action<Item> StartCloseProduct;
    private void OnEnable()
    { 
        // m_ItemSelector.onSelectedEvent += PresentItem;
        // m_InputManager.onSelectedEvent += PresentItem;
        m_RaycastSystem.OnTapSelectedEvent += PresentItem;
    }
    private void OnDisable()
    {
        // m_ItemSelector.onSelectedEvent -= PresentItem;
        // m_InputManager.onSelectedEvent -= PresentItem;
        m_RaycastSystem.OnTapSelectedEvent -= PresentItem;

    }

    public void PresentItem(ISelectable itemParent)
    {
        Debug.Log("Presented " + itemParent.GetType());
        if (m_Action) return;
        
        m_Action = true;

        _itemRotator = new ItemRotator(_rotatorConfig);
        
        var m_ItemParent = itemParent as MonoBehaviour;
        m_ItemParentTransform = m_ItemParent.transform;
        
        var m_Item = m_ItemParent.GetComponentInChildren<Item>();

        _selectedItem = m_Item;

        StartPresentation?.Invoke(m_Item);

        m_RaycastSystem.StopRaysact();
        
        m_ItemStartPosition = m_ItemParent.transform.position;
        m_ItemStartRotationEuler = m_Item.transform.localRotation.eulerAngles;
        m_Item.transform.SetParent(m_PresentPlace);
        
        Debug.Log("m_ItemStartRotationEuler " + m_ItemStartRotationEuler);

        var rot = m_PresentPlace.rotation.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        m_Item.transform.DOMove(m_PresentPlace.position, 0.5f);
        m_Item.transform.DORotate(rot, 1).OnComplete(BoxTransformRotator);
        //DOVirtual.DelayedCall(1, () => AnimationShowItem(m_Item));
        //DOVirtual.DelayedCall(11, () => ReturnItemToStartPlace(m_Item));
    }

    private void BoxTransformRotator()
    {
        _selectedItem.StartScaler();

        _itemRotator.SetTransform(_selectedItem.transform);
        _swipe.SwipeEvent += OnSwipeRotator;
        _swipe.SwipeEndEvent += OnSwipeEndEvent;
        _swipe.ShortSwipeEvent += BoxShortSwipeEvent;
        //Debug.Log("BoxTransformRotator");
    }

    private void BoxShortSwipeEvent(SwipeSide side, float power)
    {
        if (side == SwipeSide.Up && _selectedItem.ProductTransform !=null && _selectedItem.Anim !=null)
        {
            NextLevelPresentation();
        }
        else if (side == SwipeSide.Down)
        {
            ReturnItemToStartPlace(_selectedItem);
        }
        else if (side == SwipeSide.Right)
        {
            int step = Mathf.RoundToInt(_rotatorConfig.MaxRotationStep * power);

            _itemRotator.ToLeftLimit(step);
        }
        else if (side == SwipeSide.Left)
        {
            int step = Mathf.RoundToInt(_rotatorConfig.MaxRotationStep * power);

            _itemRotator.ToRightLimit(step);
        }
    }

    private void OnSwipeRotator(Vector2 rotation)
    {
        if (_itemRotator != null)
            _itemRotator.RotateTransform(rotation);
    }

    private void NextLevelPresentation()
    {
        _swipe.SwipeEvent -= OnSwipeRotator;
        _swipe.ShortSwipeEvent -= BoxShortSwipeEvent;
        _swipe.SwipeEndEvent -= OnSwipeEndEvent;

        _itemRotator.EndReturnRotation += EndBoxReturnRotation;
        _itemRotator.ReturnToStartRotation();
        _selectedItem.ReturnToStartScale();
    }

    private void EndBoxReturnRotation()
    {
        _beforeShowPosition = _selectedItem.transform.localPosition;
        Vector3 pos = _beforeShowPosition;
        pos.y -= _productShowHeight;
        _selectedItem.transform.DOLocalMove(pos, 1f);
        //Play animation
        _itemRotator.EndReturnRotation -= EndBoxReturnRotation;

        var animator =  _selectedItem.Anim;
        animator.Play("Show");
        StartOpenProduct?.Invoke(_selectedItem);
        // AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        // AnimatorClipInfo[] myAnimatorClip = animator.GetCurrentAnimatorClipInfo(0);
        // float myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
        
        DOVirtual.DelayedCall(2f, EndPresentationAnimation);
    }
    
    private void EndPresentationAnimation()
    {
        _itemRotator.SetTransform(_selectedItem.ProductTransform);
        _swipe.SwipeEvent += OnSwipeRotator;
        _swipe.ShortSwipeEvent += ProductShortSwipeEvent;
        _swipe.SwipeEndEvent += OnSwipeEndEvent;

        Debug.Log("EndPresentationAnimation");
    }
    
    private void ProductShortSwipeEvent(SwipeSide side, float power)
    {
        if (side == SwipeSide.Down)
        {
            _swipe.SwipeEvent -= OnSwipeRotator;
            _swipe.ShortSwipeEvent -= ProductShortSwipeEvent;
            _swipe.SwipeEndEvent -= OnSwipeEndEvent;

            _itemRotator.ReturnToStartRotation(HideAnimation);
            _selectedItem.ReturnToStartScale();
        }
        else if (side == SwipeSide.Right)
        {
            int step = Mathf.RoundToInt(_rotatorConfig.MaxRotationStep * power);

            _itemRotator.ToLeftLimit(step);
        }
        else if (side == SwipeSide.Left)
        {
            int step = Mathf.RoundToInt(_rotatorConfig.MaxRotationStep * power);

            _itemRotator.ToRightLimit(step);
        }
    }

    private void HideAnimation()
    {
        var animator =  _selectedItem.Anim;
        animator.Play("EndShow");
        // AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        // AnimatorClipInfo[] myAnimatorClip = animator.GetCurrentAnimatorClipInfo(0);
        // float myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
        StartCloseProduct?.Invoke(_selectedItem);
        DOVirtual.DelayedCall(2f, EndShowAnimationCallback);
    }

    private void EndShowAnimationCallback()
    {
        BoxTransformRotator();
        _selectedItem.transform.DOLocalMove(_beforeShowPosition, 1f);
        Debug.Log("EndShowAnimationCallback");
    }
    
    void AnimationShowItem(Item item, Action callback = null)
    {
        Debug.Log("ShowItem " + item.name);

        var animator =  item.GetComponent<Animator>();
        animator.Play("Show");
        DOVirtual.DelayedCall(8, () => animator.Play("EndShow")).OnComplete(() => callback?.Invoke());
    }

    private void OnSwipeEndEvent(Vector2 arg1, float arg2)
    {
        _itemRotator.SmoothNearestLimit();
    }
    
    void ReturnItemToStartPlace(Item item)
    {
        float returningTime = 1f;
        item.transform.SetParent(m_ItemParentTransform);
        item.transform.DOMove(m_ItemParentTransform.position, returningTime);
        item.transform.DOLocalRotate(m_ItemStartRotationEuler, returningTime);
        item.ReturnToStartScale(returningTime,()=>item.StopScaler());
            
        DOVirtual.DelayedCall(returningTime, () => 
        {
            m_Action = false;    
            _swipe.SwipeEvent -= OnSwipeRotator;
            _swipe.ShortSwipeEvent -= BoxShortSwipeEvent;
            _swipe.SwipeEndEvent -= OnSwipeEndEvent;

            EndPresentation?.Invoke(item);
            m_RaycastSystem.StartRaysact();

            _selectedItem = null;
        });
    }

    public void InitInput(InputManager input)
    {
        m_InputManager = input;
    }

    public void ResetPresenter()
    {
        m_Action = false;
        StartPresentation = null;
        EndPresentation = null;

        StartOpenProduct = null;
        StartCloseProduct = null;
    }

    private void Update()
    {
        _itemRotator?.Update();
    }
}
