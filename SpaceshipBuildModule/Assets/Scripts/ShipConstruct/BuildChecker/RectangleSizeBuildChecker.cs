using UnityEngine;

public class RectangleBuildChecker : AbstractBuildChecker, BuildCheckerInterface {
    public bool checkModuleForBuild(ShipNode shipNode) {
        if (shipNode == null) return false;
        
        var size = buildManager.moduleToBuild.size;
        for (int x = 0; x < size.x; x++) {
            for (int y = 0; y < size.y; y++) {
                var coordinateToBuild = new Vector3(x, y, 0) + shipNode.transform.position;
                if (!checkNodeForExisting(coordinateToBuild))
                    return false;
            }
        }
        return true;
    }
}