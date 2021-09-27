using System.Collections;
using System.Collections.Generic;
using Engenious.Core.Managers;
using UnityEngine;

public class DefaultSceneState : SceneState<DefaultSceneStatesManager,DefaultSceneStateParams>
{
    public override bool SetActivate(bool value)
    {
        if (base.SetActivate(value))
        {
            if (value)
            {
                //ActivateState();
            }
            else
            {
                //DeActivateState();
                return false;
            }
        }

        return true;
    }
}
