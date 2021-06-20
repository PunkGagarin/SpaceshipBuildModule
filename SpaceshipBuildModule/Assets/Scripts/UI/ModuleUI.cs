using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModuleUI : MonoBehaviour {
    private Scrollbar activeModuleScrollbar;
    private float lastScrollPos;

    private UnityAction<float> changeScrollposAction;
    private bool isScrollbarFrozen = false;

    public static ModuleUI GetInstance { get; private set; }

    private void Awake() {
        if (GetInstance == null) {
            GetInstance = this;
        }
        changeScrollposAction = setBarValueToLastValue;
    }

    public void setUIInactive(GameObject currentUI) {
        currentUI.SetActive(false);
        activeModuleScrollbar = null;
    }

    //return scrollbar to the left side
    public void setUIActive(GameObject nextUI) {
        activeModuleScrollbar = nextUI.GetComponentInChildren<Scrollbar>();
        nextUI.SetActive(true);
        var scrollbar = nextUI.GetComponentInChildren<Scrollbar>();
        if (scrollbar != null) {
            scrollbar.value = 0;
        }
    }

    public void freezeCurrentScrollPosition() {
        if (activeModuleScrollbar != null && !isScrollbarFrozen) {
            activeModuleScrollbar.onValueChanged.AddListener(changeScrollposAction);
            lastScrollPos = activeModuleScrollbar.value;
            if (lastScrollPos >= 1)
                lastScrollPos = 1.01f;
            else if (lastScrollPos <= 0)
                lastScrollPos = -0.01f;

            isScrollbarFrozen = true;
        }
    }

    public void unfreezeScrollPosition() {
        if (activeModuleScrollbar != null && isScrollbarFrozen) {
            activeModuleScrollbar.onValueChanged.RemoveListener(changeScrollposAction);
            isScrollbarFrozen = false;
        }
    }

    private void setBarValueToLastValue(float fakeFloat) {
        if (activeModuleScrollbar != null) {
            activeModuleScrollbar.value = lastScrollPos;
        }
    }
}