using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UserDataScripts
{
    public static class FileUtils
    {
        public static async Task<string> ReadAllTextAsync(string filePath)
        {
            string result = String.Empty;
            using (var fileStream = File.OpenRead(filePath))
            using (var streamReader = new StreamReader(fileStream))
            {
                result = await streamReader.ReadToEndAsync();
            }

            return result;
        }

        public static async void WriteAllTextAsync(string filePath, string jsonString, Action success = null)
        {
            UnicodeEncoding uniencoding = new UnicodeEncoding();

            byte[] result = uniencoding.GetBytes(jsonString);

            using (FileStream SourceStream = File.Open(filePath, FileMode.OpenOrCreate))
            {
                SourceStream.Seek(0, SeekOrigin.End);
                await SourceStream.WriteAsync(result, 0, result.Length);
                success?.Invoke();
            }
        }

        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public static async Task<T> ReadAllText<T>(string filePath)  where T: class
        {
            string json = await ReadAllTextAsync(filePath);

            T data = JsonUtility.FromJson<T>(json);
            //Debug.Log("READALLTEXT = "+data.ToString());
            return data;
        }
        
        public static void WriteAllText<T>(string filePath, T data, Action success = null) where T: class
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, json);
            success?.Invoke();
        }
    }
}