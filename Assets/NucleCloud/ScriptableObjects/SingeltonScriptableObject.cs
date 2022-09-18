using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SingeltonScriptableObject<T> : ScriptableObject where T : SingeltonScriptableObject<T>
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T[] assets = Resources.LoadAll<T>("");
                if(assets == null || assets.Length < 1)
                {
                    Debug.LogWarning("No matching scriptable object found!");
                }
                else if (assets.Length>1)
                {
                    Debug.LogWarning("Multiple matching scriptable objects found, the first one will be used!");
                }
                else
                {
                    _instance = assets[0];
                }
            }
            return _instance;
        }
    }
}