using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.AddressablesManager
{
    public class AssetReferenceUtility
    {
        public AssetReference _objectLoad;
        public AssetReference _accessoryObjectLoad;

        private GameObject _instantiateObject;
        private GameObject _instantiateAccessoryObject;

        private void Load()
        {
            Addressables.LoadAssetAsync<GameObject>(_objectLoad).Completed += OnCompleted;
        }

        private void OnCompleted(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                var loadedObj = obj.Result;

                _instantiateObject = Object.Instantiate(loadedObj);

                if (_accessoryObjectLoad != null)
                {
                    _accessoryObjectLoad.InstantiateAsync(_instantiateObject.transform).Completed += handle =>
                    {
                        if (handle.Status == AsyncOperationStatus.Succeeded)
                        {
                            _instantiateAccessoryObject = handle.Result;
                        }
                    };
                }
            }
        }
    }
}