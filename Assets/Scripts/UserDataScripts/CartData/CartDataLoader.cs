using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.UserDataScripts
{
    public interface ICartDataLoader
    {
        void Load(Action<SavedCartData> readSuccess = null, Action readFail = null);
        void Write(Action<SavedCartData> writeSuccess = null);
        void DeleteFile();
    }

    public class CartDataLoader : ICartDataLoader
    {
        private const string _fileName = "/cartData.json";

        private string _filePath;

        private SavedCartData _cartData;

        public CartDataLoader(SavedCartData cartData)
        {
            _filePath = Application.persistentDataPath + _fileName;
            _cartData = cartData;
        }
        
        public async void Load(Action<SavedCartData> readSuccess = null, Action readFail = null)
        {
            if (File.Exists(_filePath))
            {
                _cartData = await FileUtils.ReadAllText<SavedCartData>(_filePath);
                //Debug.Log("Load Cart data = " + _cartData.CartItems[0].ProductName);

                readSuccess?.Invoke(_cartData);
            }
            else
            {
                readFail?.Invoke();
            }
        }

        public async void Write(Action<SavedCartData> writeSuccess = null)
        {
            FileUtils.WriteAllText(_filePath,_cartData, () => writeSuccess?.Invoke(_cartData));
        }

        public void DeleteFile()
        {
            FileUtils.DeleteFile(_filePath);
        }
    }
}