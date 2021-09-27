using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UserDataScripts
{
    public interface IUserDataLoader
    {
        void Load(Action<SavedUserData> readSuccess = null, Action readFail = null);
        void Write(Action<SavedUserData> writeSuccess = null);
        void DeleteFile();
    }

    public class UserDataLoader : IUserDataLoader
    {
        
        private const string _fileName = "/userData.json";

        private string _filePath;

        private SavedUserData _userData;

        public UserDataLoader(SavedUserData userData)
        {
            _filePath = Application.persistentDataPath + _fileName;
            _userData = userData;
        }
        
        public async void Load(Action<SavedUserData> readSuccess = null, Action readFail = null)
        {
            if (File.Exists(_filePath))
            {
                _userData = await FileUtils.ReadAllText<SavedUserData>(_filePath);

                readSuccess?.Invoke(_userData);
            }
            else
            {
                readFail?.Invoke();
            }
            
            Debug.LogError("LoadUseData = " + _userData.ToString());
        }

        public async void Write(Action<SavedUserData> writeSuccess = null)
        {
            FileUtils.WriteAllText(_filePath,_userData, () => writeSuccess?.Invoke(_userData));
        }

        public void DeleteFile()
        {
            FileUtils.DeleteFile(_filePath);
        }
    }
}