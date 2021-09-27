using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class TimedEvent : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] bool isOnEnabled = true;

    public UnityEvent OnTimePass;
    private Tween _tween;



    private void OnEnable()
    {
        if (isOnEnabled)
        {
            Execute();
        }
    }

    public void Execute()
    {
        if (_tween!=null)
        {
            _tween.Kill();
        }
        _tween = DOVirtual.DelayedCall(time, OnEnding);
    }

    private void OnEnding()
    {
        OnTimePass?.Invoke();
    }
}
