using UnityEngine;

namespace Shop.Core.Utils
{
    public class SingletonT<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
	            if (_instance != null)
	            {
		            return _instance;
	            }
	            
	            _instance = FindObjectOfType<T>();

	            if (_instance != null)
	            {
		            return _instance;
	            }
	            
				var singleton = new GameObject("[SINGLETON]" + typeof(T));
				_instance = singleton.AddComponent<T>();

				return _instance;
            }
        }
    }
}