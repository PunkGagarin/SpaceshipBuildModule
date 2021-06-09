using UnityEngine;

public class ShipPlacer : MonoBehaviour {
    public GameObject[] shipPrefabs;
    private BuildManager buildManager;


    private void Start() {
        buildManager = BuildManager.GetInstance;

        placeShip();
    }

    private void placeShip() {
        var shipIndex = ShipChooser.currentShipIndex > shipPrefabs.Length ? 0 : ShipChooser.currentShipIndex - 1;
        if (ShipChooser.existingShips.TryGetValue(shipIndex + 1, out var existingShip)) {
            existingShip.SetActive(true);
            existingShip.transform.position = Vector3.zero;
            buildManager.currentShip = existingShip;
        }
        else {
            var shipToInstantiate = shipPrefabs[shipIndex];
            var ship = Instantiate(shipToInstantiate, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(ship);
            buildManager.currentShip = ship;
        }
    }
}