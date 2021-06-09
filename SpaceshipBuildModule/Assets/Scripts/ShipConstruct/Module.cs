using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour {
    
    public SpriteRenderer mainRenderer;
    public List<NodeDirections> directions;
    public List<ShipNode> occupiedNodes;

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

        //TODO: checkout for orderlayer priority and move from hardCode
        //mainRenderer.sortingOrder = available ? 1 : 11;
    }

    public void returnToNormalState() {
        mainRenderer.material.color = startColor;
        // mainRenderer.sortingOrder = 1;
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