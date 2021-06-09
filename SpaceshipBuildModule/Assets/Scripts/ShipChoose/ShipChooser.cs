using System.Collections.Generic;
using UnityEngine;

public class ShipChooser : MonoBehaviour {

    public static int currentShipIndex = 1;

    public static Dictionary<int, GameObject> existingShips = new Dictionary<int, GameObject>();

    public void OnShipClick(int shipNumber) {
        currentShipIndex = shipNumber;
    }
}
