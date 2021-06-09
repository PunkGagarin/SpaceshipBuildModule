using UnityEngine;
using UnityEngine.UI;

public class ModuleUI : MonoBehaviour {
    
    public void setUIInactive(GameObject currentUI) {
        currentUI.SetActive(false);
    }

    //return scrollbar to the left
    public void setUIActive(GameObject nextUI) {
        nextUI.SetActive(true);
        var scrollbar = nextUI.GetComponentInChildren<Scrollbar>();
        if (scrollbar != null) {
            scrollbar.value = 0;
        }
    }
}