using System;
using Assets.Scripts.MainWindows;
using UnityEngine;

namespace Engenious.MainScene.ViewModels
{
    public class WelcomeVM : BaseVM<WelcomeWindow>
    {
        public void SubscribeButtons(Action logInOnClick, Action signInOnClick)
        {
            _window.SubscribeButtons(logInOnClick, signInOnClick);
        }
    }
}