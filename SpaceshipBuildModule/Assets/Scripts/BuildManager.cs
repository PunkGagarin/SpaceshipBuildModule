using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildManager : MonoBehaviour {
    private List<ShipNode> existingNodes;
    private List<Module> existingModules;
    private Module temporalModule;
    private Camera mainCamera;

    #region Singleton

    public static BuildManager GetInstance { get; private set; }

    private void Awake() {
        if (GetInstance == null) {
            GetInstance = this;
        }

        existingNodes ??= new List<ShipNode>();
        existingModules ??= new List<Module>();
    }

    #endregion

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        if (temporalModule != null) {
            moveAndBuildTempModule();
        }
    }

    private bool moveAndBuildTempModule() {
        bool available = true;
        var groundPlane = new Plane(Vector3.forward, Vector3.zero);
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(ray, out var position)) {
            var worldPosition = ray.GetPoint((position));

            var x = Mathf.RoundToInt(worldPosition.x);
            var y = Mathf.RoundToInt(worldPosition.y);
            var temporalPosition = new Vector3(x, y);

            temporalModule.transform.position = temporalPosition;
            available = checkModuleForBuild(temporalPosition);

            temporalModule.setTransparent(available);

            buildModule(available, temporalPosition);
        }

        return available;
    }

    private void buildModule(bool available, Vector3 temporalPosition) {
        if (available && Input.GetMouseButtonDown(0)) {
            temporalModule.setNormal();
            setModuleNodes(temporalModule, temporalPosition);
            existingModules.Add(temporalModule);
            temporalModule = null;
        }
    }

    public void prepareTemporalModule(Module modulePrefab) {
        if (temporalModule != null) {
            Destroy(temporalModule.gameObject);
        }

        temporalModule = Instantiate(modulePrefab);
    }

    public void addExistingNode(ShipNode node) {
        existingNodes.Add(node);
    }

    private bool checkModuleForBuild(Vector3 baseCoordinate) {
        var nodeDirections = temporalModule.directions;
        return nodeDirections.Select(direction =>
            ModuleUtils.vector2Direction(direction) + baseCoordinate).All(checkNodeForExisting);
    }

    private void setModuleNodes(Module module, Vector3 baseCoordinate) {
        var nodeDirections = temporalModule.directions;

        foreach (var direction in nodeDirections) {
            Vector3 vector3 = ModuleUtils.vector2Direction(direction) + baseCoordinate;
            ShipNode node = findNodeByCoordinates(vector3);
            if (!node.isEmpty)
                cleanUpNode(node);
            node.builtModule = temporalModule;
            temporalModule.addNode(node);
        }
    }

    private void cleanUpNode(ShipNode node) {
        var nodeBuiltModule = node.builtModule;
        nodeBuiltModule.cleanUpNodes();
        existingModules.Remove(node.builtModule);
        Destroy(nodeBuiltModule.gameObject);
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

    //return node with passed coordinates or null
    private ShipNode findNodeByCoordinates(Vector3 coordinate) {
        return existingNodes.FirstOrDefault(node => coordinate.Equals(node.transform.position));
    }

    public void showAllModules() {
        foreach (var module in existingModules) {
            Debug.Log(module);
        }
    }
}