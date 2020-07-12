using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [Header ("Tab")]
    [SerializeField] private List<TabButton> tabButtons;
    [SerializeField] private Sprite tabIdle;
    [SerializeField] private Sprite tabHover;
    [SerializeField] private Sprite tabActive;
    [Header("Page")]
    [SerializeField] private List<GameObject> objectsToSwap;

    private TabButton selectedTab;

    private void Start()
    {
        OnTabSelected(tabButtons[tabButtons.Count-1]);
    }

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || selectedTab != button) button.background.sprite = tabHover;
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i=0; i<objectsToSwap.Count; i++)
        {
            if (i == index) objectsToSwap[i].SetActive(true);
            else objectsToSwap[i].SetActive(false);
        }
    }

    private void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if (selectedTab != null && selectedTab == button) continue;
            button.background.sprite = tabIdle;
        }
    }
}
