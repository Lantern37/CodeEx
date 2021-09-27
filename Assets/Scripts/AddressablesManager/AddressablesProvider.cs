using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.AddressablesManager
{
    
    public class AddressablesProvider
    {
        private Dictionary<string, AsyncOperationHandle> _completedCache = new Dictionary<string, AsyncOperationHandle>();

        private Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }
        
        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_completedCache.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;
            
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);
            
            handle.Completed += operationHandle =>
            {
                _completedCache[assetReference.AssetGUID] = operationHandle;
            };

            AddHandle(assetReference.AssetGUID, handle);

            return await handle.Task;
        }

        public async Task<T> Load<T>(string assetPath) where T : class
        {
            if (_completedCache.TryGetValue(assetPath, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;
            
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetPath);
            
            handle.Completed += operationHandle =>
            {
                _completedCache[assetPath] = operationHandle;
            };

            AddHandle(assetPath, handle);

            return await handle.Task;
        }

        public void CleanUp()
        {
            foreach (var resourceHandles in _handles.Values)
            {
                foreach (var handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }
            }
            
            _completedCache.Clear();
            _handles.Clear();
        }


        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandle))
            {
                resourceHandle = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandle;
            }

            resourceHandle.Add(handle);
        }
    }
}