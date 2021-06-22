using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IPointerDownHandler {
    public UnityEvent buildEvent { get; private set; }

    private void Awake() {
        buildEvent ??= new UnityEvent();
    }

    public void OnPointerDown(PointerEventData eventData) {
        buildEvent.Invoke();
    }
}
