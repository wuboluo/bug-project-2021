using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleMono<T> : MonoBehaviour where T:MonoBehaviour
{
    protected static T _instance = null;
    protected static readonly object _locker = new object();
    public static T GetInstance
    {
        get
        {
            if(_instance == null)
            {
                lock (_locker)
                {
                    if(_instance == null)
                    {
                        _instance = FindObjectOfType<T>();
                    }
                }
            }
            return _instance;
        }
    }
}
