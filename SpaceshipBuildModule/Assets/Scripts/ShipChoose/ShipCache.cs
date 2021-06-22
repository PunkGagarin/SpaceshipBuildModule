using System.Collections.Generic;
using UnityEngine;

public class ShipCache : MonoBehaviour {
    public static readonly Dictionary<int, GameObject> existingShips = new Dictionary<int, GameObject>();
    public static int currentShipIndex1 { get; set; } = 1;

    public void OnShipClick(int shipNumber) {
        currentShipIndex1 = shipNumber;
    }
}