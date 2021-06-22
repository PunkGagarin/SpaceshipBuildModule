using UnityEngine;

public class ShipPlacer : MonoBehaviour {
    [SerializeField] private GameObject[] shipPrefabs;
    private BuildManager buildManager;
    private NodeManager nodeManager;


    private void Start() {
        buildManager = GetComponent<BuildManager>();
        nodeManager = GetComponent<NodeManager>();

        placeShip();
    }

    private void placeShip() {
        var shipIndex = ShipCache.currentShipIndex1 > shipPrefabs.Length ? 0 : ShipCache.currentShipIndex1 - 1;
        if (ShipCache.existingShips.TryGetValue(shipIndex + 1, out var existingShip)) {
            placeExistingShip(existingShip);
        }
        else {
            placeNewShip(shipIndex);
        }
        nodeManager.initExistingNodes();
    }

    private void placeExistingShip(Ship existingShip) {
        existingShip.gameObject.SetActive(true);
        existingShip.transform.position = Vector3.zero;
        buildManager.currentShipModel = existingShip;
    }


    private void placeNewShip(int shipIndex) {
        var shipToInstantiate = shipPrefabs[shipIndex];
        var ship = Instantiate(shipToInstantiate, Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(ship);
        buildManager.currentShipModel = ship.GetComponent<Ship>();
    }
}