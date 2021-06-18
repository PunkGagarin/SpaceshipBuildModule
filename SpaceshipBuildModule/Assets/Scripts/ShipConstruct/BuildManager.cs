using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour {
    [HideInInspector] public GameObject currentShip;

    private List<ShipNode> existingNodes;

    //Currently selected module
    private Module moduleToBuild;
    private ModuleUI moduleUI;

    private Camera mainCamera;
    private Plane groundPlane;

    public static BuildManager GetInstance { get; private set; }

    private void Awake() {
        if (GetInstance == null) {
            GetInstance = this;
        }
        existingNodes ??= new List<ShipNode>();
    }

    private void Start() {
        mainCamera = Camera.main;
        groundPlane = new Plane(Vector3.forward, Vector3.zero);
        moduleUI = ModuleUI.GetInstance;
    }

    private void Update() {
        if (moduleToBuild == null) return;


        if (Input.GetMouseButton(0)) {
            moveAndBuildTempModule();
            if (!EventSystem.current.IsPointerOverGameObject())
                moduleUI.freezeCurrentScrollPosition();
        }
        if (Input.GetMouseButtonUp(0)) {
            releaseModule();
        }
    }

    private void moveAndBuildTempModule() {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(ray, out var position)) {
            var worldPosition = ray.GetPoint(position);
            roundAndCreatePosition(worldPosition);
            bool available = checkModuleForBuild(moduleToBuild.transform.position);
            moduleToBuild.setTransparent(available);
        }
    }

    private void roundAndCreatePosition(Vector3 worldPosition) {
        var x = Mathf.RoundToInt(worldPosition.x);
        var y = Mathf.RoundToInt(worldPosition.y);
        moduleToBuild.transform.position = new Vector3(x, y);
    }

    //todo: reduce calling frequency??
    private bool checkModuleForBuild(Vector3 baseCoordinate) {
        var nodeDirections = moduleToBuild.directions;
        return nodeDirections.Select(direction =>
            ModuleUtils.vector2Direction(direction) + baseCoordinate).All(checkNodeForExisting);
    }

    //return true if node with passed coordinates exists
    private bool checkNodeForExisting(Vector3 coordinate) {
        return existingNodes.Any(node => coordinate.Equals(node.transform.position));
    }

    private void releaseModule() {
        bool available = checkModuleForBuild(moduleToBuild.transform.position);
        if (available) {
            moduleToBuild.returnToNormalState();
            setModuleNodes();
            moduleToBuild.gameObject.transform.parent = currentShip.transform;
            moduleToBuild = null;
        }
        else {
            Destroy(moduleToBuild.gameObject);
        }
        moduleUI.unfreezeScrollPosition();
    }

    private void setModuleNodes() {
        var nodeDirections = moduleToBuild.directions;

        foreach (var direction in nodeDirections) {
            Vector3 vector3 = ModuleUtils.vector2Direction(direction) + moduleToBuild.transform.position;
            ShipNode node = findNodeByCoordinates(vector3);

            //TODO: potential bug?
            if (!node.isEmpty)
                cleanUpNode(node);
            node.builtModule = moduleToBuild;
            moduleToBuild.addOccupiedNode(node);
        }
    }

    //return node with passed coordinates or null
    private ShipNode findNodeByCoordinates(Vector3 coordinate) {
        return existingNodes.FirstOrDefault(node => coordinate.Equals(node.transform.position));
    }

    private void cleanUpNode(ShipNode node) {
        var nodeBuiltModule = node.builtModule;
        nodeBuiltModule.cleanUpOccupiedNodes();
        Destroy(nodeBuiltModule.gameObject);
    }

    public void prepareModuleToBuild(Module modulePrefab) {
        if (moduleToBuild != null) {
            Destroy(moduleToBuild.gameObject);
        }
        moduleToBuild = Instantiate(modulePrefab);
    }

    public void addExistingNode(ShipNode node) {
        existingNodes.Add(node);
    }

    public bool isAnyEmptyNodeExists() {
        return existingNodes.Any(node => node.isEmpty);
    }
}