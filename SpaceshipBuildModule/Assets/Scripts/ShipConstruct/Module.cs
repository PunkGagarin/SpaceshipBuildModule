using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour {
    [SerializeField] private Vector2Int _size;
    [SerializeField] private SpriteRenderer mainRenderer;
    
    private List<ShipNode> occupiedNodes;
    private Color startColor;

    private void Awake() {
        startColor = mainRenderer.material.color;
    }

    private void Start() {
        occupiedNodes ??= new List<ShipNode>();
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

    public Vector2Int size => _size;
}