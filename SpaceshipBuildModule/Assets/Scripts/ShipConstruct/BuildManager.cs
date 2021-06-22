using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour {
    [HideInInspector] public GameObject currentShip;

    //Currently selected module
    private ModuleUI moduleUI;
    private BuildCheckerInterface buildChecker;

    private Camera mainCamera;
    private Plane groundPlane;

    public static BuildManager GetInstance { get; private set; }

    public List<ShipNode> existingNodes { get; private set; }

    public Module moduleToBuild { get; private set; }

    private void Awake() {
        if (GetInstance == null) {
            GetInstance = this;
        }
        existingNodes ??= new List<ShipNode>();
        buildChecker = GetComponent<BuildCheckerInterface>();
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
            bool available = buildChecker.checkModuleForBuild(moduleToBuild.transform.position);
            moduleToBuild.setTransparent(available);
        }
    }

    private void roundAndCreatePosition(Vector3 worldPosition) {
        var x = Mathf.RoundToInt(worldPosition.x);
        var y = Mathf.RoundToInt(worldPosition.y);
        moduleToBuild.transform.position = new Vector3(x, y);
    }

    private void releaseModule() {
        bool available = buildChecker.checkModuleForBuild(moduleToBuild.transform.position);
        if (available) {
            moduleToBuild.returnToNormalState();
            setModuleNodesBySize();
            moduleToBuild.gameObject.transform.parent = currentShip.transform;
            moduleToBuild = null;
        }
        else {
            Destroy(moduleToBuild.gameObject);
        }
        moduleUI.unfreezeScrollPosition();
    }

    private void setModuleNodesByDirections() {
        var nodeDirections = moduleToBuild.directions;
        foreach (var direction in nodeDirections) {
            setModuleNodes(SpaceBuildUtils.vector2Direction(direction));
        }
    }

    private void setModuleNodesBySize() {
        for (int x = 0; x < moduleToBuild.size.x; x++) {
            for (int y = 0; y < moduleToBuild.size.y; y++) {
                Vector3 offset = new Vector3(x, y, 0);
                setModuleNodes(offset);
            }
        }
    }

    private void setModuleNodes(Vector3 offset) {
        var coordinateToBuild = offset + moduleToBuild.transform.position;
        ShipNode node = findNodeByCoordinates(coordinateToBuild);

        //TODO: potential bug?
        if (!node.isEmpty)
            cleanUpNode(node);
        node.builtModule = moduleToBuild;
        moduleToBuild.addOccupiedNode(node);
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