using System;
using UnityEditorInternal;
using UnityEngine;

namespace Common
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance => _instance;
        private static T _instance;

        private void Awake()
        {
            if(_instance) Destroy(gameObject);
            else
            {
                _instance = (T)this;
            }
        }
    }
}
