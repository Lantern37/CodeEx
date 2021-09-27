using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Engenious.Core.Managers
{
    [CreateAssetMenu(fileName = "WindowsManagerConfig", menuName = "Engenious/Core/Configs/WindowsManagerConfig", order = 1)]
    public class WindowsManagerConfig : ScriptableObject
    {
        [SerializeField] List<GameObject> _uiprefabs = new List<GameObject>();

        public List<IWindowController> Uiprefabs
        {
            get => _uiprefabs.Select(x => x.GetComponent<IWindowController>()).ToList();
        }

    }
}
