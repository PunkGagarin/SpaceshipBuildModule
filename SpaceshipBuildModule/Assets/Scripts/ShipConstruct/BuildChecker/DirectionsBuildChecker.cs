using System.Linq;

internal class DirectionsBuildChecker : AbstractBuildChecker, BuildCheckerInterface {
    public bool checkModuleForBuild(ShipNode shipNode) {
        return buildManager.moduleToBuild.directions
            .Select(direction => SpaceBuildUtils.vector2Direction(direction) + shipNode.transform.position)
            .All(checkNodeForExisting);
    }
}