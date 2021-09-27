using Engenious.Core.Managers;

namespace Engenious.MainScene.ViewModels
{
    public class BaseVM <W> where W : WindowController
    {
        protected W _window;

        public bool IsInited { get; protected set; }

        public void Initialize()
        {
            if(IsInited)
                return;

            InitInternal();
            
            IsInited = true;
        }
        
        public void SetWindow(W window)
        {
            _window = window;

            SetupWindow();
        }

        protected virtual void SetupWindow()
        {
        }

        public virtual void UpdateWindow(){}

        protected virtual void InitInternal(){}
    }
}