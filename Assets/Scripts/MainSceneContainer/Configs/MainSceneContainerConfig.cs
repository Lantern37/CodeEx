using Engenious.MainScene.Services;
using UnityEngine;

namespace Engenious.MainScene.Configs
{
    [CreateAssetMenu(fileName = "MainSceneContainerConfig", menuName = "Engenious/Configs/MainSceneContainerConfig")]
    public class MainSceneContainerConfig: ScriptableObject
    {
        //All containers configs
        public SoundManagerConfig SoundManagerConfig;
    }
}