using UnityEngine;

public class ShipPlacer : MonoBehaviour {
    public GameObject[] shipPrefabs;
    private BuildManager buildManager;


    private void Start() {
        buildManager = BuildManager.GetInstance;

        placeShip();
    }

    private void placeShip() {
        var shipIndex = ShipCache.currentShipIndex > shipPrefabs.Length ? 0 : ShipCache.currentShipIndex - 1;
        if (ShipCache.existingShips.TryGetValue(shipIndex + 1, out var existingShip)) {
            placeExistingShip(existingShip);
        }
        else {
            placeNewShip(shipIndex);
        }
    }

    private void placeExistingShip(GameObject existingShip) {
        existingShip.SetActive(true);
        existingShip.transform.position = Vector3.zero;
        buildManager.currentShip = existingShip;

        foreach (var shipNode in existingShip.GetComponentsInChildren<ShipNode>()) {
            buildManager.addExistingNode(shipNode);
        }
    }


    private void placeNewShip(int shipIndex) {
        var shipToInstantiate = shipPrefabs[shipIndex];
        var ship = Instantiate(shipToInstantiate, Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(ship);
        buildManager.currentShip = ship;
    }
}