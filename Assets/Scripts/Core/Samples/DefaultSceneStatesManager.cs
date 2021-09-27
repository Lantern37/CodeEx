using System.Collections;
using System.Collections.Generic;
using Engenious.Core;
using Engenious.Core.Managers;
using Engenious.Core.WindowsController;
using Engenious.MainScene;
using UnityEngine;
using Zenject;

public class DefaultSceneStatesManager : SceneStatesManager
{
    [field: Inject]
    public ICoreApiManager CoreApi { get; private set; }

    [field: Inject]
    public WindowsManager WindowsManager { get; }

    private void Start()
    {
        Initialize();
        ChildInitialize();
        //CoreInitialize();
    }

    protected virtual void ChildInitialize()
    {
        
    }

    protected override void Update()
    {
        base.Update();

        CoreApi.Update();
    }
}
