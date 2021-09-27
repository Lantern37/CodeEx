using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsBehaviorTick : MonoBehaviour
{
    private List<ItemPathParent> _mainList;
    private bool _isInited = false;

    private void Update()
    {
        if (!_isInited)
        {
            return;
        }
        TicksUpdate();
    }

    public void InitMainList(List<ItemPathParent> mainList)
    {
        _mainList = mainList;
        _isInited = true;
       
        // Observable.EveryUpdate()
        //     .Subscribe(x => TicksUpdate())
        //     .AddTo(this);
    }

    private void TicksUpdate()
    {
        var list = _mainList.ToList();
        if (!_isInited || list.Count < 1)
        {
            return;
        }
        foreach (var ball in list)
        {
            ball.Tick();    
        }
    }
}
