using Assets.Scripts.UserDataScripts;
using UnityEngine;

namespace Assets.Scripts.Test
{
    public class TestReadWriteFile : MonoBehaviour
    {
        [SerializeField] private string _filePath;

        [SerializeField] private SomeData _data;
        
        private string _fullPath;

        private void Start()
        {
            _fullPath = Application.persistentDataPath + _filePath;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                FileUtils.WriteAllText(_fullPath, _data);
            }
            
            if (Input.GetKeyDown(KeyCode.V))
            {
                GetData();
            }
        }

        private async void GetData()
        {
            _data = await FileUtils.ReadAllText<SomeData>(_fullPath);
        }
    }

    [System.Serializable]
    public class SomeData
    {
        public int Numb;
        public string Str;
        public bool SomeBool;
    }
}