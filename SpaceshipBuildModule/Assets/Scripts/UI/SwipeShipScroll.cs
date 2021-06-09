using UnityEngine;
using UnityEngine.UI;

public class SwipeShipScroll : MonoBehaviour {
    public Scrollbar scrollbar;

    private float scrollbarPosition;

    private float[] positions;


    private void Start() {
        positions = new float[transform.childCount];
    }

    private void Update() {
        roundButtonAndScale();
    }

    private void roundButtonAndScale() {
        float distance = 1f / (positions.Length - 1);

        for (int i = 0; i < positions.Length; i++) {
            positions[i] = distance * i;
        }

        if (Input.GetMouseButton(0)) {
            scrollbarPosition = scrollbar.value;
        }
        else {
            //Round to middle of button
            for (int i = 0; i < positions.Length; i++) {
                //i = 1
                if (scrollbarPosition < positions[i] + (distance / 2) &&
                    scrollbarPosition > positions[i] - (distance / 2)) {
                    ShipChooser.currentShipIndex = i + 1;
                    scrollbar.value =
                        Mathf.Lerp(scrollbar.value, positions[i], 0.05f);
                    transform.GetChild(i).localScale =
                        Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.3f, 1.3f), 10 * Time.deltaTime);
                }
                else {
                    transform.GetChild(i).localScale =
                        Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(.8f, .8f), 10 * Time.deltaTime);
                }
            }
        }
    }
}