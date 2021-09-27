using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Engenious.Core.Utils
{
    public class JsonUtils
    {
        public static void JsonParse<T>(string json, ref List<T> obj) where T : new()
        {
            if (!string.IsNullOrEmpty(json)) {
                IDictionary dataSearch = (IDictionary)JsonConvert.DeserializeObject(json);

                if (dataSearch["data"] != null) {
                    Debug.Log(JsonConvert.SerializeObject(dataSearch["data"]));
                    IList categoriesList = (IList)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dataSearch["data"]));

                    foreach (var elem in categoriesList) {
                        T objElem = new T()/*default*/;
                        JsonUtility.FromJsonOverwrite(JsonConvert.SerializeObject(elem), objElem);
                        obj.Add(objElem);
                    }

                    Debug.LogFormat("<color=#00ff00ff> was loaded sucsessfull!! </color>");
                }
                else {
                    Debug.LogError("not contains a data!");
                }
            }
        }
        
        public static void JsonParse<T>(string json, ref T deserObj)
        {
            if (!string.IsNullOrEmpty(json)) {                                 
                IDictionary dataSearch = (IDictionary)JsonConvert.DeserializeObject(json);

                if (dataSearch["data"] != null) {

                    Debug.Log(JsonConvert.SerializeObject(dataSearch["data"]));
                    JsonUtility.FromJsonOverwrite(JsonConvert.SerializeObject(dataSearch["data"]), deserObj);

                    Debug.LogFormat("<color=#00ff00ff> Peeps was loaded sucsessfull!! " + typeof(T).Name + "</color>");
                }
                else {
                    //deserObj = null;
                    Debug.LogError("Peeps not contains a data!");
                }
            }
        }
    }
}