using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ContentUI : MonoBehaviour {
    private RectTransform content;

    [Serializable]
    public struct ButtonItem {
        public GameObject buttonPrefab;
        public Sprite buttonSprite;
        public string buttonText;
        public UnityEvent buttonClickEvent;
        public UnityEvent buildEvent;
    }

    public ButtonItem[] buttonItems;

    private void Start() {
        content = GetComponent<RectTransform>();
        initButtons();
    }

    private void initButtons() {
        
        foreach (var buttonItem in buttonItems) {
            var instance = Instantiate(buttonItem.buttonPrefab, content, false);
            if (buttonItem.buttonSprite != null)
                instance.transform.Find("InnerImage").GetComponent<Image>().sprite = buttonItem.buttonSprite;
            if (!string.IsNullOrEmpty(buttonItem.buttonText)) {
                instance.GetComponentInChildren<Text>().text = buttonItem.buttonText;
            }
            if (buttonItem.buttonClickEvent != null)
                instance.GetComponent<Button>().onClick.AddListener(buttonItem.buttonClickEvent.Invoke);
            if (buttonItem.buildEvent.GetPersistentEventCount() > 0) {
                instance.GetComponent<DragNDrop>().buildEvent.AddListener(buttonItem.buildEvent.Invoke);
            }
        }
    }
}