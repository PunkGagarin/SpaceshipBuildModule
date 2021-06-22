using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour {
    [HideInInspector] public Ship currentShipModel;
    [SerializeField] private LayerMask nodeLayer;

    //Currently selected module
    private ModuleUI moduleUI;
    private BuildCheckerInterface buildChecker;
    private NodeManager nodeManager;

    private Camera mainCamera;
    private Plane groundPlane;

    public Module moduleToBuild { get; private set; }

    public static BuildManager GetInstance { get; private set; }

    private void Awake() {
        if (GetInstance == null) {
            GetInstance = this;
        }
        buildChecker = GetComponent<BuildCheckerInterface>();
        nodeManager = GetComponent<NodeManager>();
    }

    private void Start() {
        mainCamera = Camera.main;
        groundPlane = new Plane(Vector3.forward, Vector3.zero);
        moduleUI = ModuleUI.GetInstance;
    }

    private void Update() {
        if (moduleToBuild == null) return;

        if (Input.GetMouseButton(0)) {
            moveAndBuildTempModule();
            if (!EventSystem.current.IsPointerOverGameObject())
                moduleUI.freezeCurrentScrollPosition();
        }
        if (Input.GetMouseButtonUp(0)) {
            releaseModule();
        }
    }

    private ShipNode castToGetNode() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, nodeLayer.value);
        if (hit.transform != null) {
            //TODO: get node to the future processing
        }
        return null;
    }

    private void moveAndBuildTempModule() {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(ray, out var position)) {
            var worldPosition = ray.GetPoint(position);
            roundAndCreatePosition(worldPosition);
            bool available = false;
            if (isPointOverNode(out var node))
                available = buildChecker.checkModuleForBuild(node);
            moduleToBuild.setTransparent(available);
        }
    }

    private bool isPointOverNode(out ShipNode shipNode) {
        shipNode = null;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(SpaceBuildUtils.roundVector2(ray.origin),
            SpaceBuildUtils.roundVector2(ray.direction), Mathf.Infinity, nodeLayer.value);
        if (hit.transform != null) {
            shipNode = hit.transform.gameObject.GetComponent<ShipNode>();
            return true;
        }
        return false;
    }

    private void roundAndCreatePosition(Vector3 worldPosition) {
        moduleToBuild.transform.position = SpaceBuildUtils.roundVector3(worldPosition);
    }

    private void releaseModule() {
        bool available = false;
        if (isPointOverNode(out var node))
            available = buildChecker.checkModuleForBuild(node);
        if (available) {
            moduleToBuild.returnToNormalState();
            setModuleNodesBySize();
            // moduleToBuild.gameObject.transform.parent = currentShip.transform;
            currentShipModel.modules.Add(moduleToBuild);
            moduleToBuild.gameObject.transform.parent = currentShipModel.transform;

            moduleToBuild = null;
        }
        else {
            Destroy(moduleToBuild.gameObject);
        }
        moduleUI.unfreezeScrollPosition();
    }

    private void setModuleNodesBySize() {
        for (int x = 0; x < moduleToBuild.size.x; x++) {
            for (int y = 0; y < moduleToBuild.size.y; y++) {
                var offset = new Vector3(x, y, 0);
                var coordinateToBuild = offset + moduleToBuild.transform.position;
                var node = nodeManager.findNodeByCoordinates(coordinateToBuild);
                setModuleNodes(node);
            }
        }
    }

    private void setModuleNodes(ShipNode node) {
        //TODO: potential bug?
        if (!node.isEmpty)
            nodeManager.cleanUpNode(node);
        node.builtModule = moduleToBuild;
        moduleToBuild.addOccupiedNode(node);
    }

    public void prepareModuleToBuild(Module modulePrefab) {
        if (moduleToBuild != null) {
            Destroy(moduleToBuild.gameObject);
        }
        moduleToBuild = Instantiate(modulePrefab);
    }
}