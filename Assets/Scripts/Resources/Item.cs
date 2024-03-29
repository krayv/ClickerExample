using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string Name;
    public string Description;
    public int ID;
    public int Order;

    public static Dictionary<int, TValue> ConvertSOKeyDictionaryToIDKeyDictionary<TKey,TValue>(Dictionary<TKey, TValue> dictionary) where TKey : Item
    {
        Dictionary<int, TValue> convertedDictionary = new Dictionary<int, TValue>();
        foreach (var keyValuePair in dictionary)
        {
            convertedDictionary.Add(keyValuePair.Key.ID, keyValuePair.Value);
        }
        return convertedDictionary;
    }

    public static Dictionary<int, TValue> ConvertSOKeyDictionaryToIDKeyDictionary<TKey, TValue>(ReactiveDictionary<TKey, TValue> dictionary) where TKey : Item
    {
        Dictionary<int, TValue> convertedDictionary = new Dictionary<int, TValue>();
        foreach (var keyValuePair in dictionary)
        {
            convertedDictionary.Add(keyValuePair.Key.ID, keyValuePair.Value);
        }
        return convertedDictionary;
    }
}
