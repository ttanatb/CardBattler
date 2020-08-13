using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance_ = null;

    public static T Instance
    {
        get
        {
            if (instance_ == null)
            {
                GameObject ownerObj = new GameObject(typeof(T).Name);
                instance_ = ownerObj.AddComponent<T>();

                DontDestroyOnLoad(ownerObj);
            }
            return instance_;
        }
    }
}
