using System;
using UnityEngine;
using UnityEngine.UI;

public class SwipeShipScroll : MonoBehaviour {
    public GameObject scrollbar;

    private float scrollbarPosition;

    private float[] positions;


    private void Start() {
        positions = new float[transform.childCount];
    }

    private void Update() {
        float distance = 1f / (positions.Length - 1);

        for (int i = 0; i < positions.Length; i++) {
            positions[i] = distance * i;
        }

        if (Input.GetMouseButton(0)) {
            scrollbarPosition = scrollbar.GetComponent<Scrollbar>().value;
        }
        else {
            //Round to middle of button
            for (int i = 0; i < positions.Length; i++) {
                //i = 1
                if (scrollbarPosition < positions[i] + (distance / 2) &&
                    scrollbarPosition > positions[i] - (distance / 2)) {
                    scrollbar.GetComponent<Scrollbar>().value =
                        Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, positions[i], 0.05f);
                }
            }
        }

        for (int i = 0; i < positions.Length; i++) {
            if (scrollbarPosition < positions[i] + (distance / 2) &&
                scrollbarPosition > positions[i] - (distance / 2)) {
                transform.GetChild(i).localScale =
                    Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.3f, 1.3f), 0.01f);
            }
            else {
                transform.GetChild(i).localScale =
                    Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(.8f, .8f), 0.01f);
            }
        }
    }
}