using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour {
    public SpriteRenderer mainRenderer;
    //TODO: move to different class(implement 2 classes)
    public List<NodeDirections> directions;
    public Vector2Int size;
    
    
    private List<ShipNode> occupiedNodes;
    private Color startColor;

    private void Awake() {
        startColor = mainRenderer.material.color;
    }

    private void Start() {
        occupiedNodes ??= new List<ShipNode>();
        directions ??= new List<NodeDirections>();
        directions.Add(NodeDirections.Center);
    }

    public void setTransparent(bool available) {
        mainRenderer.material.color = available ? Color.green : Color.red;
    }

    public void returnToNormalState() {
        mainRenderer.material.color = startColor;
    }

    public void addOccupiedNode(ShipNode node) {
        node.isEmpty = false;
        occupiedNodes.Add(node);
    }

    public void cleanUpOccupiedNodes() {
        foreach (var node in occupiedNodes) {
            node.isEmpty = true;
            node.builtModule = null;
        }

        occupiedNodes.Clear();
    }
}