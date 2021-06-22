using UnityEngine;
using UnityEngine.UI;

public class SwipeShipScroll : MonoBehaviour {
    [SerializeField]
    private Scrollbar scrollbar;

    private float scrollbarPosition;
    private float[] positions;
    private float distance;


    private void Start() {
        positions = new float[transform.childCount];

        initScrollbarPositions();
    }

    private void initScrollbarPositions() {
        distance = 1f / (positions.Length - 1);
        for (int i = 0; i < positions.Length; i++) {
            positions[i] = distance * i;
        }
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            scrollbarPosition = scrollbar.value;
        }
        else {
            roundButtonAndScale();
        }
    }

    //Round current scrollbar position to the center of nearest button and set scales
    private void roundButtonAndScale() {
        for (int i = 0; i < positions.Length; i++) {
            if (scrollbarPosition < positions[i] + (distance / 2) &&
                scrollbarPosition > positions[i] - (distance / 2)) {
                ShipCache.currentShipIndex1 = i + 1;
                scrollbar.value =
                    Mathf.Lerp(scrollbar.value, positions[i], 0.05f);
                lerpButtonScales(i, 1.3f, 1.3f);
            }
            else {
                lerpButtonScales(i, .8f, .8f);
            }
        }
    }

    private void lerpButtonScales(int i, float x, float y) {
        transform.GetChild(i).localScale =
            Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(x, y), 10 * Time.deltaTime);
    }
}