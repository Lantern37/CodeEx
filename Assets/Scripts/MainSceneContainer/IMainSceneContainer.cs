using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engenious.Core;
using Engenious.MainScene.Configs;
using Engenious.MainScene.Services;
using Engenious.MainScene.ViewModels;
using UnityEngine;
using Zenject;

namespace Engenious.MainScene
{
    /// <summary>
    /// Container for all containers of Main scene
    /// </summary>
    public interface IMainSceneContainer
    {
        ICoreApiManager CoreApi { get; }

        /// <summary>
        /// Space for all components
        /// </summary>
        IMainSceneViewModels MainSceneViewModels { get; }

        IMainSceneModels MainSceneModels { get; } 

        IMainSceneServices MainSceneServices { get; } 
        
        MainSceneObjects MainSceneObjects { get; }
        //Configs for all main scene containers
        MainSceneContainerConfig Config { get; set; }
        
        bool IsInited { get; }
        Task Initialize();

        void Update();
    }

    public class MainSceneContainer : IMainSceneContainer
    {
        [Inject]
        public MainSceneObjects MainSceneObjects { get; set; }

        [Inject]
        public MainSceneContainerConfig Config { get; set; }

        [Inject]
        public ICoreApiManager CoreApi { get; }
        public IMainSceneServices MainSceneServices { get; private set; }
        public IMainSceneModels MainSceneModels { private set; get; }
        public IMainSceneViewModels MainSceneViewModels { get; private set; }

        public bool IsInited { get; private set; }

        /// <summary>
        /// For initing all containers components
        /// </summary>
        public async Task Initialize()
        {
            if (!IsInited)
            {
                CreateReferences();
                await InitReferences();
                
                IsInited = true;
            }
        }

        /// <summary>
        /// Create and Setup
        /// </summary>
        private void CreateReferences()
        {
            MainSceneViewModels = new MainSceneViewModels(this);
            MainSceneModels = new MainSceneModels(this);
            MainSceneServices = new MainSceneServices(this);
        }

        private async Task InitReferences()
        {
            await MainSceneServices.Initialize();
            await MainSceneModels.Initialize();
            MainSceneViewModels.Initialize();
        }
        
        /// <summary>
        /// For updating all containers components
        /// </summary>
        public void Update()
        {
            if (!IsInited)
                return;
            
        }
    }
}
