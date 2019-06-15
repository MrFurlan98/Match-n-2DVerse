using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour {

    private Image m_headToSend;
    private Image m_armToSend;
    private Image m_legToSend;
    private Image m_bodyToSend;

    private int m_indexButton;

    private Vector2Int m_BoardAspect = new Vector2Int(7, 7);

    public int IndexButton
    {
        get
        {
            return m_indexButton;
        }

        set
        {
            m_indexButton = value;
        }
    }

    public void OpenBoard()
    {
      
        LevelManager.Instance.CurrentLevel = m_indexButton;

        ScreenManager.Instance.OpenScreen(ScreenManager.SCREEN.GAMEPLAY);
    }
}
