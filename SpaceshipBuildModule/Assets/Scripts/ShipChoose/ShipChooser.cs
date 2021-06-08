using UnityEngine;

public class ShipChooser : MonoBehaviour {

    public static int currentShip = 1;

    public void OnShipClick(int shipNumber) {
        currentShip = shipNumber;
    }
}
