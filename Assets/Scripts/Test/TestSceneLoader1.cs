using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Test
{
    public class TestSceneLoader1 : MonoBehaviour
    {
        [Inject] private ITestSceneCont _scene;

        [SerializeField] private KeyCode _debug = KeyCode.G;
        
        private void Update()
        {
            if (Input.GetKeyDown(_debug))
            {
                _scene.DebugSome();
            }
        }
    }
}