using UnityEngine;
using Zenject;

namespace Assets.Scripts.Test
{
    public interface ITestSceneCont
    {
        void DebugSome();
    }
    
    
    public class TestSceneCont : ITestSceneCont
    {
        [Inject] private ITestProjectCont _projCont;
                public void DebugSome()
                {
                    Debug.Log("Scene");
                    _projCont.DebugSome();
                }

    }
}