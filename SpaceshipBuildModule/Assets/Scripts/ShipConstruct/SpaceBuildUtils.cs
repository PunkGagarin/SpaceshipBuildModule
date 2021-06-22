using System;
using System.Collections.Generic;
using UnityEngine;

public static class SpaceBuildUtils {

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

    public static Vector2 roundVector2(Vector2 vector2) {
        var x = Mathf.RoundToInt(vector2.x);
        var y = Mathf.RoundToInt(vector2.y);
        return new Vector2(x, y);
    }
    public static Vector2 roundVector3(Vector3 vector3) {
        var x = Mathf.RoundToInt(vector3.x);
        var y = Mathf.RoundToInt(vector3.y);
        return new Vector3(x, y);
    }
}