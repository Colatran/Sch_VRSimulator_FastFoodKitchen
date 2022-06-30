using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] UIMainMenuPanel[] panels;

    private UIMainMenuPanel currentPanel;
    private int currentPanelIndex;



    private void Start()
    {
        currentPanel = panels[0];
        currentPanelIndex = 0;

        animator.SetTrigger("Open");
    }

    public void SetPanel(int panel)
    {
        if (panel == currentPanelIndex) return;

        currentPanelIndex = panel;
        animator.SetTrigger("Close");
    }

    public void OnFinishClose()
    {
        currentPanel.gameObject.SetActive(false);

        currentPanel = panels[currentPanelIndex];
        currentPanel.gameObject.SetActive(true);

        animator.SetTrigger("Open");
    }
}
