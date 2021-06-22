using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeManager : MonoBehaviour {
    private BuildManager buildManager;
    public List<ShipNode> existingNodes { get; private set; }

    public static NodeManager GetInstance { get; private set; }

    private void Awake() {
        if (GetInstance == null) {
            GetInstance = this;
        }
        existingNodes ??= new List<ShipNode>(); 
        buildManager = GetComponent<BuildManager>();
    }

    public ShipNode findNodeByCoordinates(Vector3 coordinate) {
        return existingNodes.FirstOrDefault(node => coordinate.Equals(node.transform.position));
    }

    public void addExistingNode(ShipNode node) {
        existingNodes.Add(node);
        buildManager.currentShipModel.nodes.Add(node);
    }

    public bool isAnyEmptyNodeExists() {
        return existingNodes.Any(node => node.isEmpty);
    }

    public void initExistingNodes() {
        foreach (var node in existingNodes) {
            node.initNeighbours();
        }
    }

    public void cleanUpNode(ShipNode node) {
        var nodeBuiltModule = node.builtModule;
        nodeBuiltModule.cleanUpOccupiedNodes();
        Destroy(nodeBuiltModule.gameObject);
    }
}