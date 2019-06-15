using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class PauseUI : MonoBehaviour {
    [SerializeField]
    private Button m_bMenu;

    [SerializeField]
    private Button m_pResume;

    private void Start()
    {
        UpdateMenuButton();
        UpdateResumeButton();
    }

    private void UpdateMenuButton()
    {
        m_bMenu.onClick = new Button.ButtonClickedEvent();
        m_bMenu.onClick.AddListener(SelectMenuButton);
    }

    private void UpdateResumeButton()
    {
        m_pResume.onClick = new Button.ButtonClickedEvent();
        m_pResume.onClick.AddListener(SelectResumeButton);
    }

    private void SelectMenuButton()
    {
        UIManager.Instance.OpenScreen(UIManager.SCREEN.MENU);
        UIManager.Instance.CloseScreen(UIManager.SCREEN.PAUSE);
    }

    private void SelectResumeButton()
    {
        UIManager.Instance.CloseScreen(UIManager.SCREEN.PAUSE);
    }


}
