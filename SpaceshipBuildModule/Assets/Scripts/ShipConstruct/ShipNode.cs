using UnityEngine;

public class ShipNode : MonoBehaviour {
    private NodeManager nodeManager;
    public ShipNode topNeighbour { get; private set; }
    public ShipNode rightNeighbour { get; private set; }
    public bool isEmpty { get; set; } = true;

    public Module builtModule { get; set; }

    private void Start() {
        GetComponentInParent<Ship>().nodes.Add(this);
    }

    private void OnEnable() {
        nodeManager = NodeManager.GetInstance;
        nodeManager.addExistingNode(this);
    }

    public void initNeighbours() {
        topNeighbour = nodeManager.findNodeByCoordinates(
            SpaceBuildUtils.vector2Direction(NodeDirections.Top) + transform.position
        );
        rightNeighbour = nodeManager.findNodeByCoordinates(
            SpaceBuildUtils.vector2Direction(NodeDirections.Right) + transform.position
        );
    }
}