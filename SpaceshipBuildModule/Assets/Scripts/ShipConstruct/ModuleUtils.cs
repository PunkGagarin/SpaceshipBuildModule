using System;
using System.Collections.Generic;
using UnityEngine;

public static class ModuleUtils {

    public static Vector3 vector2Direction(NodeDirections direction) {
        return direction switch
        {
            NodeDirections.Top => Vector3.up,
            NodeDirections.TopRight => new Vector3(1, 1),
            NodeDirections.Right => Vector3.right,
            NodeDirections.BottomRight => new Vector3(1, -1),
            NodeDirections.Bottom => Vector3.down,
            NodeDirections.BottomLeft => new Vector3(-1, -1),
            NodeDirections.Left => Vector3.left,
            NodeDirections.TopLeft => new Vector3(-1, 1),
            NodeDirections.Center => Vector3.zero,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "No such direction")
        };
    }
    
    public static bool tryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key))
        {
            return false;
        }
        dictionary.Add(key, value);
        return true;
    }
    public static bool tryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, KeyValuePair<TKey,TValue> value)
    {
        return tryAdd(dictionary, value.Key, value.Value);
    }
}