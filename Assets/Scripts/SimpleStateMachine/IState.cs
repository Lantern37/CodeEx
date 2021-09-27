namespace Assets.Scripts.SimpleStateMachine
{
    public interface IState
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsFinished { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsActivated { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsPaused { get; }

        /// <summary>
        /// SuspendInBack GameState if it locate not on top queue
        /// </summary>
        bool SuspendInBack { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_params"></param>
        void Setup(ISimpleStateParams _params);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetPaused(bool value);

        /// <summary>
        /// 
        /// </summary>
        bool SetActivate(bool value);

        /// <summary>
        /// 
        /// </summary>
        void Execute();
    }
}