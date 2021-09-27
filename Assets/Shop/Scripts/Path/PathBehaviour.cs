using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

public class PathBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float m_SplineLength;
    [SerializeField] private GameObject m_MiddleSelectorPrefab;

    private int m_CurrentMagnetIndex;
    public float SplineLength {
        get => m_SplineLength;
        set
        {
             m_SplineLength = value;
        }
}

    private float m_DistanceBetweenItems;

    public float DistanceBetweenItems 
    {
        get => m_DistanceBetweenItems;
        set
        {
            m_DistanceBetweenItems = value;
        }
    }

    private SplineComputer m_Spline;
    // private List<ItemPathParent> m_currentLable;

    public List<ItemPathParent> CurrentLable
    {
        get;
        set;
    }

    private List<double> m_MagnetPoints = new List<double>();
    private float m_HeadDistance;
    private GameObject m_CenterSelector;

    private List<Tweener> m_MoveTweeners = new List<Tweener>();
    private bool m_InitIsDone = false;
    private bool m_IsMoving;
    private bool m_LongSwipe;
    private bool m_ShortSwipe;

    public bool CanSwipe
    {
        get;
        set;
    }
    
    // public bool SwipeMoving
    // {
    //     set => m_IsMoving = value;
    // }
    
    public bool LongSwipe
    {
        set => m_LongSwipe = value;
    }

    
    public void Init(SplineComputer spline , float distanceBetweenItems)
    {
        CurrentLable = new List<ItemPathParent>();
        m_Spline = spline;
        m_SplineLength = spline.CalculateLength();
        
        //Debug.Log("....Init spline length " + m_SplineLength);
        m_DistanceBetweenItems = distanceBetweenItems;
        // m_MagnetPoints = GetMagnetSplinePositions();
        //Debug.Log("GetMagnetSplinePositions   init" );
        m_InitIsDone = true;
    }

   
    private void Update()
    {
        if (!m_InitIsDone ) return;
        
        foreach (var item in CurrentLable)
        { 
            item.Tick();
        }
    }

   
    public void ShowLable()
    {
        if (CurrentLable.Count > 0)
        {
            m_MagnetPoints = GetMagnetSplinePositions();
            
            //middle magnet point position
            int index = m_MagnetPoints.Count / 2;
            double positionTo = m_MagnetPoints[index];
            m_CurrentMagnetIndex = index;
            DoMoveHeadToPosition(positionTo);
        }
    }
    
   
    
    // private List<ItemPathParent> GetLocalHeads()
    // {
    //     var list = new List<ItemPathParent>();
    //     foreach (var item in CurrentLable)
    //         if (item.IsLocalHead)
    //             list.Add(item);
    //     return list;
    // }

    // private void MoveToSplinePosition(ItemPathParent item)
    // {
    //     var pos = (float) item.Positioner.position;
    //     DOTween.To(
    //         () => pos,
    //         x => pos = x,
    //         item.SplinePosition,
    //         0.1f);
    //     item.Positioner.SetDistance(pos);
    // }
    
    private int GetLocalHeadIndexInFront(int fromIndex)
    {
        for (var i = fromIndex - 1; i >= 0; i--)
            if (CurrentLable[i].IsLocalHead)
                return i;
        return -1;
    }

    
    //for Long Swipe in update
    public void MoveHeadLongSwipe(float delta)
    {
        if ( !m_LongSwipe) return;
        var currentPosition = (float)CurrentLable[0].Positioner.position;
        currentPosition += delta;
        // currentPosition = Mathf.Clamp(currentPosition, 0, m_SplineLength + (CurrentLable.Count * DistanceBetweenItems));

        CurrentLable[0].Positioner.position = currentPosition;
    }

    void DoMoveHeadToPosition(double position)
    {
        //Debug.Log("Do Move to pos " + position);
        m_IsMoving = true;
        var head = CurrentLable[0];
        Tweener tweener = DOTween.To(
            () => head.Positioner.position,
            x => head.Positioner.position = x,
            position,
            0.5f).SetEase(Ease.Unset).OnComplete(() =>
        {
            m_IsMoving = false;
           KillAllTweners();
        });
        m_MoveTweeners.Add(tweener);
    }

    void  MoveHeadToCloseMagnet(double closeMagnetPosition)
    {
        DoMoveHeadToPosition(closeMagnetPosition);
    }

   
    public void ShortSwipeToNextMagnet(SwipeSide swipeSide, float power)
    {
        if (swipeSide == SwipeSide.Down || swipeSide == SwipeSide.Up) return;
        
        m_ShortSwipe = true;
        //Debug.Log("2 MoveShortSwipe " + swipeSide);
        var localHeadIndex = -1;
        ItemPathParent localHead = null;
        localHeadIndex = GetLocalHeadIndexInFront(CurrentLable.Count);

        // m_ShortSwipe = true;
        localHead = CurrentLable[localHeadIndex];
        double endValue = ClosestMagnetInDirection(swipeSide);
        //Debug.Log("2 endValue " + endValue);

        var duration = 1 / power;
        //Debug.Log("2 duration " + duration);
        
        Tweener tweener = DOTween.To(
            () => localHead.Positioner.position,
            x => localHead.Positioner.position = x,
            endValue,
            0.5f).SetEase(Ease.Unset).OnComplete(() => m_ShortSwipe = false);
        
        m_MoveTweeners.Add(tweener);
    }
    
    
    public void MoveShortSwipe(SwipeSide swipeSide, float power, float duration)
    {
        //Debug.Log("1 MoveShortSwipe " + swipeSide);

        if (swipeSide == SwipeSide.Down || swipeSide == SwipeSide.Up) return;
        // var head = CurrentLable[0];
        //Debug.Log("2 MoveShortSwipe " + swipeSide);
        var localHeadIndex = -1;
        ItemPathParent localHead = null;
        localHeadIndex = GetLocalHeadIndexInFront(CurrentLable.Count);

        // m_ShortSwipe = true;
        localHead = CurrentLable[localHeadIndex];
        float a = 0.2f;
        double endValue = 0;
        
        if (swipeSide == SwipeSide.Left)
        {
            //Debug.Log("Left MoveShortSwipe " + swipeSide);

            endValue = localHead.Positioner.position;
            endValue -= power * a;
        }
        else if (swipeSide == SwipeSide.Right)
        {
            //Debug.Log("Right MoveShortSwipe " + swipeSide);

            endValue = localHead.Positioner.position;
            endValue += power * a;
        }
        
        Tweener tweener = DOTween.To(
            () => localHead.Positioner.position,
            x => localHead.Positioner.position = x,
            endValue,
            duration).SetEase(Ease.Unset).OnComplete(() =>
        {
            m_ShortSwipe = false;
            double currentPosition = localHead.Positioner.position;
            var closeMagnetPosition = ClosestMagnetPosition(m_MagnetPoints, currentPosition);
            MoveHeadToCloseMagnet(closeMagnetPosition);
        });
        m_MoveTweeners.Add(tweener);
    }
   
    public void EndTouch()
    {
//        Debug.Log("EndTouch  " );
        if (m_ShortSwipe || !m_LongSwipe)
        {
            return;
        }

            //Debug.Log("EndTouch was Long... " );
            KillAllTweners();
            m_LongSwipe = false;
            var currentPosition = (float)CurrentLable[0].Positioner.position;
            var closestMagnetPosition = ClosestMagnetPosition(m_MagnetPoints, currentPosition);
            MoveHeadToCloseMagnet(closestMagnetPosition);
    }

    double ClosestMagnetPosition(List<double> magnets, double currentPosition)
    {
        //Debug.Log("ClosestMagnetPosition  " );

        // double closestPosition = magnets.Aggregate((x,y) => Math.Abs(x-currentPosition) < Math.Abs(y-currentPosition) ? x : y);
        List<double> myList = magnets; 
        double myDouble = currentPosition;
        
        //Find the closest one
        double closest = myList.OrderBy(item => Math.Abs(myDouble - item)).First();
        //Debug.Log("Close magnet position   " + closest);
        //Find the second closest one
        // double secondColosest = myList.OrderBy(item => Math.Abs((colosest + 1) - item)).First();


        m_CurrentMagnetIndex = myList.FindIndex(i => i.Equals(closest));
        //Debug.Log("m_CurrentMagnetIndex  "  + m_CurrentMagnetIndex);

        return closest;
    }

    double ClosestMagnetInDirection(SwipeSide direction)
    {
        double magnetPosition = 0;
        int index = 0;

        if (direction == SwipeSide.Left)
        {
            index = m_CurrentMagnetIndex - 1;
            if (index < 0)
            {
                index = 0;
            }
        }
        else if (direction == SwipeSide.Right)
        {
            index = m_CurrentMagnetIndex + 1;
            if (index >= m_MagnetPoints.Count)
            {
                index = m_MagnetPoints.Count - 1;
            }
        }

        m_CurrentMagnetIndex = index;
        //Debug.Log("CloseetMagnetInDirection index " + index);

        magnetPosition = m_MagnetPoints[index];
        return magnetPosition;
    }
    List<double> GetMagnetSplinePositions()
    {
        var slinePositions = new List<double>();
        m_SplineLength = m_Spline.CalculateLength();
        double position = m_SplineLength / 2;

        var qtyMagnetPoints = CurrentLable.Count;
        if (qtyMagnetPoints <= 0)
        {
            qtyMagnetPoints = 20;
        }
        for (int i = 0; i < qtyMagnetPoints; i++)
        {
            
            //Debug.Log("GetMagnetSplinePosition  " + i + "  "   + position);

            slinePositions.Add(position);
            position += DistanceBetweenItems;
        }
        
       //Debug.Log("GetMagnetSplinePositions  " + slinePositions.Count);
        //Debug.Log("m_SplineLength  " + m_SplineLength);
        if (m_CenterSelector != null)
        {
            Destroy(m_CenterSelector);
        }
        InitMiddleSelector();
        return slinePositions;
    }

    void InitMiddleSelector()
    {
        m_CenterSelector = Instantiate(m_MiddleSelectorPrefab, transform);
        var positioner = m_CenterSelector.AddComponent<SplinePositioner>();
        positioner.spline = m_Spline;
        positioner.mode = SplinePositioner.Mode.Distance;
        positioner.motion.offset = new Vector2(0, 0.5f);
        positioner.position = SplineLength / 2;
    }

   
    public void KillAllTweners()
    {
        if(m_MoveTweeners.Count > 0)
        {
            foreach (var tweener in m_MoveTweeners)
            {
                tweener.Kill();
            }

            m_MoveTweeners.Clear();
        }
    }
  
}
