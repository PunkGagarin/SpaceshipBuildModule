using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildManager : MonoBehaviour {
    private List<ShipNode> existingNodes;
    private List<Module> builtModules;

    //Currently selected module
    private Module moduleToBuild;
    
    private Camera mainCamera;
    private Plane groundPlane;

    public static BuildManager GetInstance { get; private set; }

    //todo: moveout some logic????
    private void Awake() {
        if (GetInstance == null) {
            GetInstance = this;
        }

        existingNodes ??= new List<ShipNode>();
        builtModules ??= new List<Module>();
    }

    private void Start() {
        mainCamera = Camera.main;
        groundPlane = new Plane(Vector3.forward, Vector3.zero);
    }

    private void Update() {
        if (moduleToBuild != null) {
            moveAndBuildTempModule();
        }
    }

    private void moveAndBuildTempModule() {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(ray, out var position)) {
            var worldPosition = ray.GetPoint((position));
            roundAndCreatePosition(worldPosition);
            var available = checkModuleForBuild(moduleToBuild.transform.position);
            moduleToBuild.setTransparent(available);
            buildModule(available);
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
        foreach (var node in existingNodes) {
            if (coordinate.Equals(node.transform.position)) {
                return true;
            }
        }

        return false;
    }

    private void buildModule(bool available) {
        if (available && Input.GetMouseButtonDown(0)) {
            moduleToBuild.returnToNormalState();
            setModuleNodes();
            builtModules.Add(moduleToBuild);
            moduleToBuild = null;
        }
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
        builtModules.Remove(nodeBuiltModule);
        Destroy(nodeBuiltModule.gameObject);
    }

    public void prepareTemporalModule(Module modulePrefab) {
        if (moduleToBuild != null) {
            Destroy(moduleToBuild.gameObject);
        }

        moduleToBuild = Instantiate(modulePrefab);
    }

    public void addExistingNode(ShipNode node) {
        existingNodes.Add(node);
    }

    //TODO: delete after tests
    public void showAllModules() {
        foreach (var module in builtModules) {
            Debug.Log(module);
        }
    }
}