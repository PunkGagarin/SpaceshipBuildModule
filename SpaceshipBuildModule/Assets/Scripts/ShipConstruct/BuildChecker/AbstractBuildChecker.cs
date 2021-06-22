using System.Linq;
using UnityEngine;

public abstract class AbstractBuildChecker : MonoBehaviour {

    protected BuildManager buildManager;
    private NodeManager nodeManager;

    private void Start() {
        buildManager = BuildManager.GetInstance;
        nodeManager = NodeManager.GetInstance;
    }
    
    protected bool checkNodeForExisting(Vector3 coordinate) {
        return nodeManager.existingNodes
            .Any(node => coordinate.Equals(node.transform.position));
    } 
    
    protected bool checkNodeForExisting(ShipNode node) {
        return nodeManager.existingNodes
            .Any(existingNode => existingNode.Equals(node));
    }
}