using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IPointerDownHandler {

    public UnityEvent myUnityEvent;
 
    private void Awake() {
        myUnityEvent ??= new UnityEvent();
    }

    public void OnPointerDown(PointerEventData eventData) {
        myUnityEvent.Invoke();
    }
}
