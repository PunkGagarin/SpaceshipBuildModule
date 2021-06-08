using System;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour {
    public SpriteRenderer mainRenderer;
    private Color startColor;
    public List<NodeDirections> directions;
    public List<ShipNode> nodes;

    private void Awake() {
        startColor = mainRenderer.material.color;
    }

    private void Start() {
        nodes ??= new List<ShipNode>();
        directions ??= new List<NodeDirections>();
        directions.Add(NodeDirections.Center);
    }

    public void setTransparent(bool available) {
        mainRenderer.material.color = available ? Color.green : Color.red;

        //TODO: move from hardCode and checkout for orderlayer priority
        //mainRenderer.sortingOrder = available ? 1 : 11;
    }

    public void setNormal() {
        mainRenderer.material.color = startColor;
        // mainRenderer.sortingOrder = 1;
    }

    public void addNode(ShipNode node) {
        node.isEmpty = false;
        nodes.Add(node);
    }

    public void cleanUpNodes() {
        foreach (var node in nodes) {
            node.isEmpty = true;
            node.builtModule = null;
        }

        nodes.Clear();
    }
}