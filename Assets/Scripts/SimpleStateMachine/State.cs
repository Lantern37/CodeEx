namespace Assets.Scripts.SimpleStateMachine
{
    public class State<ParamType> : IState where ParamType: ISimpleStateParams
    {
        public bool IsFinished { get; set; }
        public bool IsActivated { get; private set; }
        public bool IsPaused { get; private set; }
        public bool SuspendInBack { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        protected ParamType Params;
        
        public virtual void Setup(ISimpleStateParams _params)
        {
            Params = (ParamType) _params;
        }

        public virtual bool SetPaused(bool value)
        {
            if (!value && !IsPaused)
                return false;

            if (value && IsPaused)
                return false;

            IsPaused = value;

            return true;
        }

        public virtual bool SetActivate(bool value)
        {
            if (!value && !IsActivated)
                return false;

            if (value && IsActivated)
                return false;

            IsActivated = value;

            if (!value)
                IsFinished = false;

            return true;
        }

        public void Execute()
        {
            if (IsPaused)
                return;
            
            Execute();
        }
    }
    
    public interface ISimpleStateParams
    {
    }

    public class DefaultSimpleStateParams : ISimpleStateParams
    {
    }
}