using System.Collections;
using UnityEngine;

public class AcceptBuild : MonoBehaviour {
    [SerializeField]
    private GameObject emptyNodeUI;

    private BuildManager buildManager;
    private NodeManager nodeManager;
    private SceneChanger sceneChanger;


    private void Start() {
        buildManager = GetComponent<BuildManager>();
        nodeManager = GetComponent<NodeManager>();
        sceneChanger = GetComponent<SceneChanger>();
    }

    public void tryToAccept() {
        if (nodeManager.isAnyEmptyNodeExists()) {
            emptyNodeUI.SetActive(true);
            StartCoroutine(disableEmptyNodeUI());
            return;
        }
        ShipCache.existingShips.tryAdd(ShipCache.currentShipIndex1, buildManager.currentShip);
        buildManager.currentShip.SetActive(false);
        sceneChanger.GoToShipChoiceScene();
    }

    private IEnumerator disableEmptyNodeUI() {
        yield return new WaitForSeconds(3.5f);
        emptyNodeUI.SetActive(false);
    }
}