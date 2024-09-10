using UnityEngine;

namespace Network.Particle.Scripts.Singleton
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        public bool isDontDestroy;

        private static T instance;

        //单例不销毁时第二次实例化是否Destroy
        public bool DestroyOnAwake { get; private set; }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                    {
                        Debug.Log(typeof(T) + " is not Awake");
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (CheckInstance() && isDontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        protected bool CheckInstance()
        {
            if (this == Instance)
            {
                return true;
            }

            Destroy(this);
            Destroy(gameObject);
            DestroyOnAwake = true;
            return false;
        }

        protected virtual void OnDestroy()
        {
            if (!DestroyOnAwake)
            {
                instance = null;
            }
        }
    }
}