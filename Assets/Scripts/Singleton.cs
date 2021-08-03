using System;
using UnityEngine;

// 実装はここを参照https://gist.github.com/Buravo46/f1c2c712772db09111cb
public abstract class Singleton<T> where T: class, new()
{
    private static T instance;
    private static object syncObj = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncObj)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }
            return instance;
        }
    }
}
