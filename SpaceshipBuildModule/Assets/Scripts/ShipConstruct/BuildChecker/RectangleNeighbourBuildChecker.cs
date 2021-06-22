public class RectangleNeighbourBuildChecker : AbstractBuildChecker, BuildCheckerInterface {
    public bool checkModuleForBuild(ShipNode shipNode) {
        if (shipNode == null) {
            return false;
        }
        var size = buildManager.moduleToBuild.size;
        var currentNodeToCheck = shipNode;
        var topNode = shipNode.topNeighbour;
        for (int y = 0; y < size.y; y++) {
            for (int x = 0; x < size.x; x++) {
                if (!checkNodeForExisting(currentNodeToCheck))
                    return false;
                currentNodeToCheck = currentNodeToCheck?.rightNeighbour;
            }
            currentNodeToCheck = topNode;
            topNode = currentNodeToCheck?.topNeighbour;
        }
        return true;
    }
}