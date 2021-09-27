using UnityEngine;
using Zenject;

namespace Assets.Scripts.Test
{
    public interface ITestProjectCont
    {
        void DebugSome();
    }
    
    public class TestProjectCont : ITestProjectCont
    {
        public void DebugSome()
        {
            Debug.Log("Project");
        }
    }

}