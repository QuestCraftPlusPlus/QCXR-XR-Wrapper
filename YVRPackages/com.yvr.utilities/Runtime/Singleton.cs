public abstract class Singleton<T> where T : Singleton<T>, new()
{
    private static object s_Locker = new object();

    private static T s_Instance;

    public static T instance
    {
        get
        {
            if (s_Instance == null)
            {
                lock (s_Locker)
                {
                    if (s_Instance == null)
                    {
                        s_Instance = new T();
                        s_Instance.OnInit();
                    }
                }
            }

            return s_Instance;
        }
    }

    public static T createdInstance => s_Instance;

    protected virtual void OnInit() { }
}