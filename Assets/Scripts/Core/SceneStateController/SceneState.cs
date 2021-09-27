using UnityEngine;
using Zenject;

namespace Engenious.Core.Managers
{
    public abstract class SceneState<StatesController,ParamType> : MonoBehaviour, ISceneState
        where ParamType : ISceneStateParams
        where StatesController : SceneStatesManager
    {
        [Inject] private SceneStatesManager _statesManager;

        public StatesController StatesManager
        {
            get
            {
                return _statesManager as StatesController;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActivated { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// Suspend state if it locate not on top queue
        /// </summary>
        public bool SuspendInBack { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        protected ParamType Params;

        /// <summary>
        /// 
        /// </summary>
        protected virtual float LifeTime => 0;

        /// <summary>
        /// 
        /// </summary>
        protected virtual float UpdateTrigger => 0f;

        /// <summary>
        /// 
        /// </summary>
        private float _executeTime;

        /// <summary>
        /// 
        /// </summary>
        private float _triggerTime;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_params"></param>
        public virtual void Setup(ISceneStateParams _params)
        {
            Params = (ParamType) _params;
        }

        /// <summary>
        /// 
        /// </summary>
        void ISceneState.Execute()
        {
            if (IsPaused)
                return;

            if (LifeTime > 0)
            {
                _executeTime += Time.deltaTime;
                if (_executeTime > LifeTime)
                {
                    IsFinished = true;
                    return;
                }
            }

            _triggerTime += Time.deltaTime;
            if (_triggerTime < UpdateTrigger)
                return;

            _triggerTime = 0f;

            Execute();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool SetPaused(bool value)
        {
            if (!value && !IsPaused)
                return false;

            if (value && IsPaused)
                return false;

            IsPaused = value;

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        public virtual bool SetActivate(bool value)
        {
            if (!value && !IsActivated)
                return false;

            if (value && IsActivated)
                return false;

            IsActivated = value;
            gameObject.SetActive(value);

            if (!value)
                IsFinished = false;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Execute()
        {
        }
    }
}