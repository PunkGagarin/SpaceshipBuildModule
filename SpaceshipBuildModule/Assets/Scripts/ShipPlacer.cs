using UnityEngine;

public class ShipPlacer : MonoBehaviour {

    public GameObject[] shipPrefabs;

    private void Start() {
        var shipNumber = ShipChooser.currentShip > shipPrefabs.Length ? 0 : ShipChooser.currentShip - 1;
        Instantiate(shipPrefabs[shipNumber], Vector3.zero, Quaternion.identity);
    }
}
