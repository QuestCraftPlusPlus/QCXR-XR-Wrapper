using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void Call(params object[] objs);

public class EventManager
{
    private static Dictionary<string, List<System.Object>> _mapT0 = new Dictionary<string, List<System.Object>>();
    private static Dictionary<string, List<System.Object>> _mapT1 = new Dictionary<string, List<System.Object>>();
    private static Dictionary<string, List<System.Object>> _mapT2 = new Dictionary<string, List<System.Object>>();
    private static Dictionary<string, List<System.Object>> _mapT3 = new Dictionary<string, List<System.Object>>();
    private static Dictionary<string, List<System.Object>> _mapT4 = new Dictionary<string, List<System.Object>>();

    private static void AddListenerT(string name, Action callback)
    {
        if (string.IsNullOrEmpty(name) || callback == null)
        {
            Debug.LogError("EventManager AddListener failed,invalid  name or null callback");
            return;
        }

        List<object> calls;
        if (_mapT0.TryGetValue(name, out calls))
        {
            if (calls.Contains(callback))
                Debug.LogError("EventManager AddListener failed," + callback + "callback is exist");
            else
                calls.Add(callback);
        }
        else
        {
            calls = new List<object>();
            calls.Add(callback);
            _mapT0.Add(name, calls);
        }
    }

    private static void AddListenerT<T>(string name, Action<T> callback)
    {
        if (string.IsNullOrEmpty(name) || callback == null)
        {
            Debug.LogError("EventManager AddListener failed,invalid  name or null callback");
            return;
        }

        List<object> calls;
        if (_mapT1.TryGetValue(name, out calls))
        {
            if (calls.Contains(callback))
                Debug.LogError("EventManager AddListener failed," + callback + "callback is exist");
            else
                calls.Add(callback);
        }
        else
        {
            calls = new List<object>();
            calls.Add(callback);
            _mapT1.Add(name, calls);
        }
    }

    private static void AddListenerT<T, T1>(string name, Action<T, T1> callback)
    {
        if (string.IsNullOrEmpty(name) || callback == null)
        {
            Debug.LogError("EventManager AddListener failed,invalid  name or null callback");
            return;
        }

        List<object> calls;
        if (_mapT2.TryGetValue(name, out calls))
        {
            if (calls.Contains(callback))
                Debug.LogError("EventManager AddListener failed," + callback + "callback is exist");
            else
                calls.Add(callback);
        }
        else
        {
            calls = new List<object>();
            calls.Add(callback);
            _mapT2.Add(name, calls);
        }
    }

    private static void AddListenerT<T, T1, T2>(string name, Action<T, T1, T2> callback)
    {
        if (string.IsNullOrEmpty(name) || callback == null)
        {
            Debug.LogError("EventManager AddListener failed,invalid  name or null callback");
            return;
        }

        List<object> calls;
        if (_mapT3.TryGetValue(name, out calls))
        {
            if (calls.Contains(callback))
                Debug.LogError("EventManager AddListener failed," + callback + "callback is exist");
            else
                calls.Add(callback);
        }
        else
        {
            calls = new List<object>();
            calls.Add(callback);
            _mapT3.Add(name, calls);
        }
    }

    private static void AddListenerT<T, T1, T2, T3>(string name, Action<T, T1, T2, T3> callback)
    {
        if (string.IsNullOrEmpty(name) || callback == null)
        {
            Debug.LogError("EventManager AddListener failed,invalid  name or null callback");
            return;
        }

        List<object> calls;
        if (_mapT4.TryGetValue(name, out calls))
        {
            if (calls.Contains(callback))
                Debug.LogError("EventManager AddListener failed," + callback + "callback is exist");
            else
                calls.Add(callback);
        }
        else
        {
            calls = new List<object>();
            calls.Add(callback);
            _mapT4.Add(name, calls);
        }
    }

    private static void RemoveListenerT(string name, Action callback)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("EventManager RemoveListener failed,invalid  name");
            return;
        }

        if (_mapT0.ContainsKey(name))
        {
            if (callback == null)
                _mapT0.Remove(name);
            else
            {
                List<object> calls;
                if (_mapT0.TryGetValue(name, out calls))
                {
                    if (calls.Contains(callback))
                        calls.Remove(callback);
                    else
                        Debug.LogError("EventManager RemoveListener failed,callback not exist");
                }
                else
                    Debug.LogError("EventManager RemoveListener failed,callbacks not exist");
            }

        }
        else
            Debug.LogError("EventManager AddListener failed," + name + " event not exist");
    }
    private static void RemoveListenerT<T>(string name, Action<T> callback)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("EventManager RemoveListener failed,invalid  name");
            return;
        }

        if (_mapT1.ContainsKey(name))
        {
            if (callback == null)
                _mapT1.Remove(name);
            else
            {
                List<object> calls;
                if (_mapT1.TryGetValue(name, out calls))
                {
                    if (calls.Contains(callback))
                        calls.Remove(callback);
                    else
                        Debug.LogError("EventManager RemoveListener failed,callback not exist");
                }
                else
                    Debug.LogError("EventManager RemoveListener failed,callbacks not exist");
            }

        }
        else
            Debug.LogError("EventManager AddListener failed," + name + " event not exist");
    }
    private static void RemoveListenerT<T, T1>(string name, Action<T, T1> callback)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("EventManager RemoveListener failed,invalid  name");
            return;
        }

        if (_mapT2.ContainsKey(name))
        {
            if (callback == null)
                _mapT2.Remove(name);
            else
            {
                List<object> calls;
                if (_mapT2.TryGetValue(name, out calls))
                {
                    if (calls.Contains(callback))
                        calls.Remove(callback);
                    else
                        Debug.LogError("EventManager RemoveListener failed,callback not exist");
                }
                else
                    Debug.LogError("EventManager RemoveListener failed,callbacks not exist");
            }

        }
        else
            Debug.LogError("EventManager AddListener failed," + name + " event not exist");
    }
    private static void RemoveListenerT<T, T1, T2>(string name, Action<T, T1, T2> callback)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("EventManager RemoveListener failed,invalid  name");
            return;
        }

        if (_mapT3.ContainsKey(name))
        {
            if (callback == null)
                _mapT3.Remove(name);
            else
            {
                List<object> calls;
                if (_mapT3.TryGetValue(name, out calls))
                {
                    if (calls.Contains(callback))
                        calls.Remove(callback);
                    else
                        Debug.LogError("EventManager RemoveListener failed,callback not exist");
                }
                else
                    Debug.LogError("EventManager RemoveListener failed,callbacks not exist");
            }

        }
        else
            Debug.LogError("EventManager AddListener failed," + name + " event not exist");
    }
    private static void RemoveListenerT<T, T1, T2, T3>(string name, Action<T, T1, T2, T3> callback)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("EventManager RemoveListener failed,invalid  name");
            return;
        }

        if (_mapT4.ContainsKey(name))
        {
            if (callback == null)
                _mapT4.Remove(name);
            else
            {
                List<object> calls;
                if (_mapT4.TryGetValue(name, out calls))
                {
                    if (calls.Contains(callback))
                        calls.Remove(callback);
                    else
                        Debug.LogError("EventManager RemoveListener failed,callback not exist");
                }
                else
                    Debug.LogError("EventManager RemoveListener failed,callbacks not exist");
            }

        }
        else
            Debug.LogError("EventManager AddListener failed," + name + " event not exist");
    }


    private static void BrocastT(string name)
    {
        List<object> calls;
        if (_mapT0.TryGetValue(name, out calls))
        {
            for (int i = 0; i < calls.Count; i++)
            {
                object c = calls[i];
                var act = c as Action;
                if (act == null)
                {
                    continue;
                }
                act.Invoke();
            }
        }
        else
            Debug.LogWarning("EventManager brocast failed," + name + " event not exist");
    }
    private static void BrocastT<T>(string name, T t1)
    {
        List<object> calls;
        if (_mapT1.TryGetValue(name, out calls))
        {
            for (int i = 0; i < calls.Count; i++)
            {
                object c = calls[i];
                var act = c as Action<T>;
                if (act == null)
                {
                    continue;
                }
                act.Invoke(t1);
            }
        }
    }
    private static void BrocastT<T, T1>(string name, T t, T1 t1)
    {
        List<object> calls;
        if (_mapT2.TryGetValue(name, out calls))
        {
            for (int i = 0; i < calls.Count; i++)
            {
                object c = calls[i];
                var act = c as Action<T, T1>;
                if (act == null)
                {
                    continue;
                }
                act.Invoke(t, t1);
            }
        }
    }
    private static void BrocastT<T, T1, T2>(string name, T t, T1 t1, T2 t2)
    {
        List<object> calls;
        if (_mapT3.TryGetValue(name, out calls))
        {
            for (int i = 0; i < calls.Count; i++)
            {
                object c = calls[i];
                var act = c as Action<T, T1, T2>;
                if (act == null)
                {
                    continue;
                }
                act.Invoke(t, t1, t2);
            }
        }
    }
    private static void BrocastT<T, T1, T2, T3>(string name, T t, T1 t1, T2 t2, T3 t3)
    {
        List<object> calls;
        if (_mapT4.TryGetValue(name, out calls))
        {
            for (int i = 0; i < calls.Count; i++)
            {
                object c = calls[i];
                var act = c as Action<T, T1, T2, T3>;
                if (act == null)
                {
                    continue;
                }
                act.Invoke(t, t1, t2, t3);
            }
        }
    }


    public static void AddListenerT(Enum em, Action callback)
    {
        AddListenerT(em.ToString(), callback);
    }
    public static void AddListenerT<T>(Enum em, Action<T> callback)
    {
        AddListenerT<T>(em.ToString(), callback);
    }
    public static void AddListenerT<T, T1>(Enum em, Action<T, T1> callback)
    {
        var name = em.ToString();
        AddListenerT(name, callback);
    }
    public static void AddListenerT<T, T1, T2>(Enum em, Action<T, T1, T2> callback)
    {
        AddListenerT(em.ToString(), callback);
    }
    public static void AddListenerT<T, T1, T2, T3>(Enum em, Action<T, T1, T2, T3> callback)
    {
        AddListenerT(em.ToString(), callback);
    }

    public static void RemoveListenerT(Enum em, Action callback)
    {
        RemoveListenerT(em.ToString(), callback);
    }
    public static void RemoveListenerT<T>(Enum em, Action<T> callback)
    {
        RemoveListenerT(em.ToString(), callback);
    }
    public static void RemoveListenerT<T, T1>(Enum em, Action<T, T1> callback)
    {
        RemoveListenerT(em.ToString(), callback);
    }
    public static void RemoveListenerT<T, T1, T2>(Enum em, Action<T, T1, T2> callback)
    {
        RemoveListenerT(em.ToString(), callback);
    }
    public static void RemoveListenerT<T, T1, T2, T3>(Enum em, Action<T, T1, T2, T3> callback)
    {
        RemoveListenerT(em.ToString(), callback);
    }
 
    public static void BrocastT(Enum em)
    {
        BrocastT(em.ToString());
    }

    public static void BrocastT<T>(Enum em, T t)
    {
        BrocastT<T>(em.ToString(), t);
    }

    public static void BrocastT<T, T1>(Enum em, T t, T1 t1)
    {
        BrocastT(em.ToString(), t, t1);
    }

    public static void BrocastT<T, T1, T2>(Enum em, T t, T1 t1, T2 t2)
    {
        BrocastT(em.ToString(), t, t1, t2);
    }

    public static void BrocastT<T, T1, T2, T3>(Enum em, T t, T1 t1, T2 t2, T3 t3)
    {
        BrocastT(em.ToString(), t, t1, t2, t3);
    }
}