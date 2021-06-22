using UnityEngine;

public class ShipNode : MonoBehaviour {
    private BuildManager buildManager;

    //delegate to BuildManager?
    public bool isEmpty { get; set; }

    //Module, that was built on this node
    public Module builtModule { get; set; }

    //TODO: should node contain neighbour nodes????


    private void Awake() {
        isEmpty = true;
    }

    private void Start() {
        buildManager = BuildManager.GetInstance;
        buildManager.addExistingNode(this);
    }
}