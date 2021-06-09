using System.Collections;
using UnityEngine;

public class AcceptBuild : MonoBehaviour {
    public GameObject emptyNodeUI;

    private BuildManager buildManager;
    private SceneChanger sceneChanger;


    private void Start() {
        buildManager = GetComponent<BuildManager>();
        sceneChanger = GetComponent<SceneChanger>();
    }

    public void tryToAccept() {
        if (buildManager.isAnyEmptyNode()) {
            emptyNodeUI.SetActive(true);
            StartCoroutine(disableEmptyNodeUI());
            return;
        }
        ShipCache.existingShips.tryAdd(ShipCache.currentShipIndex, buildManager.currentShip);
        buildManager.currentShip.SetActive(false);
        sceneChanger.GoToShipChoiceScene();
    }

    private IEnumerator disableEmptyNodeUI() {
        yield return new WaitForSeconds(3);
        emptyNodeUI.SetActive(false);
    }
}