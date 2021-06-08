using UnityEngine;

public class ShipNode : MonoBehaviour {
    private BuildManager buildManager;

    //delegate to BuildManager?
    public bool isEmpty;

    private ShipNode instance;

    //Module, that was built on this node
    public Module builtModule;

    //TODO: should node contain neighbour nodes????


    private void Awake() {
        instance ??= this;
        isEmpty = true;
    }

    private void Start() {
        buildManager = BuildManager.GetInstance;
        buildManager.addExistingNode(instance);
    }
}