using UnityEngine;

public abstract class MonoBehaviorSingleton<T> : MonoBehaviour where T : MonoBehaviorSingleton<T>
{
    private bool m_Initialized = false;

    private static object s_Locker = new object();
    private static T s_Instance;

    public static T instance
    {
        get
        {
            if (!s_Instance)
            {
                // Ensure Thread Safety
                lock (s_Locker)
                {
                    if (!s_Instance)
                    {
                        s_Instance = FindObjectOfType<T>();
                        if (!s_Instance)
                        {
                            // When there is no such object in the scene, create an object that will not be destroyed with the scene switch to add the component,
                            // so as to ensure that it is not empty when used
                            s_Instance = new GameObject(typeof(T).Name).AddComponent<T>();
                            Debug.Log($"The [{typeof(T).Name}] type was not found in the scene, a new one was created");
                            DontDestroyOnLoad(s_Instance.gameObject);
                        }

                        if (s_Instance != null && !s_Instance.m_Initialized)
                            s_Instance.Init();
                    }
                }
            }

            return s_Instance;
        }
    }

    public static T createdInstance => s_Instance;

    protected virtual void Init() { m_Initialized = true; }

    protected virtual void Start()
    {
        if (!m_Initialized)
            Init();
    }
}