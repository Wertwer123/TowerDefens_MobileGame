using UnityEngine;

namespace General.Base
{
    public class Singleton<T> : MonoBehaviour where  T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>(FindObjectsInactive.Exclude);
                    if (_instance == null)
                    {
                        _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.Log($"instance already exists, instance: {_instance.name} got destroyed");
                Destroy(gameObject);
            }
        }
    }
}