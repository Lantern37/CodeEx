using Zenject;
using System;
using System.Threading.Tasks;
using Engenious.Core.Managers;
using UnityEngine;

namespace Engenious.Core
{
    public interface ICoreApiManager 
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// 
        /// </summary>
        CoreConfig ApiConfig { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task Initialize();

        /// <summary>
        /// 
        /// </summary>
        void Update();


        /// <summary>
        /// AR interface
        /// </summary>
        IARManager AR { get; set; }

        /// <summary>
        /// Network
        /// </summary>
        INetworkManager NetworkManager { get; set; }
        
        /// <summary>
        /// Download resources
        /// </summary>
        IResourcesManager ResourcesManager { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CoreApiManager : IDisposable, ICoreApiManager
    {

        [Inject] public IARManager AR { get; set; }
        
        [Inject] public INetworkManager NetworkManager { get; set; }
        [Inject] public IResourcesManager ResourcesManager { get; set; }

        [Inject] public CoreConfig ApiConfig { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public async Task Initialize()
        {
            if (!IsInitialized)
            {
                Debug.LogFormat("<color=#ffff00ff> Core manager initialization ... </color>");
                await NetworkManager.Initialize();
                //AR.Initialize();
                ResourcesManager.Initialize();

                Debug.LogFormat("<color=#ffff00ff> Core manager initialized ... </color>");

                IsInitialized = true;
            }
        }
         
        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            if (IsInitialized)
            {
                //if (AR.IsInitialized)
                    //AR.Update();
                
                if(NetworkManager.IsInitialized)
                    NetworkManager.Update();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }
    }
}