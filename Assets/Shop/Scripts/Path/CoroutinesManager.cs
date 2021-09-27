using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shop.Core.Utils;
using UnityEngine;

    public  class CoroutinesManager : SingletonT<CoroutinesManager>
    {
        // public static CoroutinesManager instance;

        private Dictionary<string, List<IEnumerator>> routinesDictionary = new Dictionary<string, List<IEnumerator>>();
        // private void Awake()
        // {
        //     instance = this;
        // }

        public  void Add(IEnumerator coroutine , string name)
        {
            // Debug.Log("Add and Start Coroutine name  " + name);
            AddToDictionary(coroutine, name);
            StartCoroutine(coroutine);
        }

        public void AddToDictionary(IEnumerator routine, string name)
        {
            string key = name;
            List <IEnumerator> val;
            if (routinesDictionary.TryGetValue(key, out val))
            {
                val.Add(routine);
                // Debug.Log("Coroutine added to dictionary ............"  + key);
            }
            else
            {
                List<IEnumerator> routinesList = new List<IEnumerator>();
                routinesList.Add(routine);
                routinesDictionary.Add(key, routinesList);
                // Debug.Log("Coroutine NEW list added to dictionary ............." + key);
            }
        }

        public void StopCoroutines(string name)
        {
            if (!routinesDictionary.ContainsKey(name))
            {
                return;
            }
            
            List <IEnumerator> val;
            if (routinesDictionary.TryGetValue(name, out val))
            {
                foreach (var routine  in val)
                {
                    StopCoroutine(routine);
                }
            }
            routinesDictionary.Remove(name);
        }

        public void StopAllCoroutines()
        {
            foreach (var key  in routinesDictionary.Keys)
            {
                Debug.Log("StopAllCoroutines   " + key);
                List <IEnumerator> val;
                if (routinesDictionary.TryGetValue(key, out val))
                {
                    foreach (var routine  in val.ToList())
                    {
                        StopCoroutine(routine);
                        Debug.Log("StopCoroutine   "  );
                    }
                }
                routinesDictionary.Remove(key);
            }
        }
    }
