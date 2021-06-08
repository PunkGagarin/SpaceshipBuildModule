using System;
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

    public static Vector2 convertToVector2(Vector3 vector3) {
        return new Vector2(vector3.x, vector3.y);
    }
}