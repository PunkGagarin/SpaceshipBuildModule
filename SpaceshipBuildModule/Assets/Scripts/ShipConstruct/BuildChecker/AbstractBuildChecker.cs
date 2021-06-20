using System.Linq;
using UnityEngine;

public abstract class AbstractBuildChecker : MonoBehaviour {

    protected BuildManager buildManager;

    private void Start() {
        buildManager = BuildManager.GetInstance;
    }
    
    protected bool checkNodeForExisting(Vector3 coordinate) {
        return buildManager.existingNodes
            .Any(node => coordinate.Equals(node.transform.position));
    }
}