﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScenaryItemUI : MonoBehaviour {
    [SerializeField]
    private Image m_pScenaryImage;
    [SerializeField]
    private Button m_pSelectButton;

    public void SelectedItem()
    {
        // get component game play UI to set background UI
        //GamePlayUI tGamePlayUI = UIManager.Instance.GetScreenObject(UIManager.SCREEN.GAMEPLAY).GetComponent<GamePlayUI>();
        

        //GameManager.Instance.ResetBoard();

        //Open UI game play and close menu UI
        //ScreenManager.Instance.OpenScreen(ScreenManager.SCREEN.GAMEPLAY);

        //UIManager.Instance.CloseScreen(UIManager.SCREEN.MENU);
    }

    public void SetScenarySprite(Sprite pScenary)
    {
        m_pScenaryImage.sprite = pScenary;
    }

    public void UpdateSelectButton()
    {
        m_pSelectButton.onClick = new Button.ButtonClickedEvent();
        m_pSelectButton.onClick.AddListener(SelectedItem);
    }

    public Image PScenaryImage
    {
        get
        {
            return m_pScenaryImage;
        }

        set
        {
            m_pScenaryImage = value;
        }
    }
}
