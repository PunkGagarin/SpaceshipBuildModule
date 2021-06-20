using System.Linq;
using UnityEngine;

public class RectangleBuildChecker : AbstractBuildChecker, BuildCheckerInterface {
    //Our rectangle always goes from top to down and from left to right
    public bool checkModuleForBuild(Vector3 baseCoordinate) {
        var size = buildManager.moduleToBuild.size;
        for (int x = 0; x < size.x; x++) {
            for (int y = 0; y < size.y; y++) {
                var coordinateToBuild = new Vector3(x, y, 0) + baseCoordinate;
                if (!checkNodeForExisting(coordinateToBuild))
                    return false;
            }
        }
        return true;
    }
}