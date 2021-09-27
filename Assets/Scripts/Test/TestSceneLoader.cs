using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Test
{
    public class TestSceneLoader : MonoBehaviour
    {
        [Inject] private ITestProjectCont _scene;
        
        [SerializeField] private string _sceneName;

        [SerializeField] private KeyCode _loadKeyCode = KeyCode.G;
        [SerializeField] private KeyCode _debugKeyCode = KeyCode.A;
        
        private void Update()
        {
            if (Input.GetKeyDown(_loadKeyCode))
            {
                SceneManager.LoadScene(_sceneName);
            }
            
            if (Input.GetKeyDown(_debugKeyCode))
            {
                _scene.DebugSome();
            }
        }
    }
}