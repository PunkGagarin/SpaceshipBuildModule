using System.Linq;
using UnityEngine;

internal class DirectionsBuildChecker : AbstractBuildChecker, BuildCheckerInterface {
    public bool checkModuleForBuild(Vector3 baseCoordinate) {
        return buildManager.moduleToBuild.directions
            .Select(direction => ModuleUtils.vector2Direction(direction) + baseCoordinate)
            .All(checkNodeForExisting);
    }
}