using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engenious.Core.Managers
{
    public abstract class SceneStatesManager : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly List<ISceneState> _states = new List<ISceneState>();
        
        /// <summary>
        /// 
        /// </summary>
        protected void Initialize()
        {
            _states.Clear();
            _states.AddRange(gameObject.GetComponentsInChildren<ISceneState>(true));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_params"></param>
        /// <returns></returns>
        public T ActivateState<T>(ISceneStateParams _params) where T : class, ISceneState
        {
            var state = _states.FirstOrDefault(s => (s as T) != null);
            if (state == null)
            {
                Debug.Log("State is absent: " + typeof(T).Name);
                return null;
            }

            state.Setup(_params);
            state.SetActivate(true);
            return state as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DeactivateState<T>() where T: class, ISceneState
        {
            var state = _states.FirstOrDefault(s => s as T != null && s.IsActivated);
            if (state != null)
                state.IsFinished = true;
        }

        /// <summary> 
        /// 
        /// </summary>
        protected virtual void Update()
        {
            if (_states.Count > 0)
            {
                var up = true;

                _states.ForEach(gameState =>
                {
                    if (gameState.IsFinished)
                    {
                        gameState.SetActivate(false);
                        return;
                    }

                    if (gameState.IsPaused || !gameState.IsActivated)
                        return;

                    if (up)
                    {
                        gameState.Execute();
                        up = false;
                    }
                    else
                    {
                        if (!gameState.SuspendInBack)
                            gameState.Execute();
                    }
                });
            }
            else
            {
                // if (_stopAllStates != null)
                // {
                //     _stopAllStates.Resolve();
                //     _stopAllStates = null;
                // }
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        // public Promise StopAllStates()
        // {
        //     _stopAllStates = new Promise();
        //     _states.ForEach(s => s.IsFinished = true);
        //     return _stopAllStates;
        // }
        

        /// <summary>
        /// 
        /// </summary>
        public void OnDestroy()
        {
        }
    }
}