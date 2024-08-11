using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILink : MonoBehaviour 
{
    public static Dictionary<string, GameObject> SuperNode = new Dictionary<string, GameObject>();
    public string key;
    private void Awake()
    {
        Debug.Log("AWAK!" + gameObject.name);
        if(string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
        {
            key = gameObject.name;
        }
        if (!SuperNode.ContainsKey(key))
        {
            SuperNode.Add(key, this.gameObject);
        }
        else
        {
            Debug.LogWarning($"Key '{key}' already exists in Components dictionary.");
        }
        
    }
    
    public static T LinkGetComponent<T>(Enum enumKey) where T : UnityEngine.Component
    {

        string key = enumKey.ToString();
        if (SuperNode.TryGetValue(key, out var value))
        {
            return value.GetComponent<T>();
        }

        Debug.LogWarning($"No GameObject found with key '{key}' in Components dictionary.");
        return null;
    }
    public static void LinkSetComponent<T>(out T componet, T check, Enum enumKey) where T : UnityEngine.Component
    {
        componet = check;
        if (componet)
            return;
        string key = enumKey.ToString();
        if (SuperNode.TryGetValue(key, out var value))
        {
            componet = value.GetComponent<T>();
            return;
        }

        Debug.LogWarning($"No GameObject found with key '{key}' in Components dictionary.");
    }

    public static void LinkSetComponent<T>(out T componet, T check, string key) where T : UnityEngine.Object
    {
        componet = check;
        if (componet)
            return;
        if (SuperNode.TryGetValue(key, out var value))
        {
            
            componet = value.GetComponent<T>();
            return;
        }

        Debug.LogWarning($"No GameObject found with key '{key}' in Components dictionary.");
    }
}
