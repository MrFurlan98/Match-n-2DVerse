using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayUI : MonoBehaviour {

    [SerializeField]
    private Image m_iBackground;

    [SerializeField]
    private Button m_bPause;

    [SerializeField]
    private Text m_tScore;

    [SerializeField]
    private Text m_tMoves;

    [SerializeField]
    private Board m_pBoard;

    [SerializeField]
    private MonsterAnimation m_pMonsterAnimation;

    private void Start()
    {
        UpdateMenuButton();
    }

    private void UpdateMenuButton()
    {
        m_bPause.onClick = new Button.ButtonClickedEvent();
        m_bPause.onClick.AddListener(SelectPauseButton);
    }

    private void SelectPauseButton()
    {
        UIManager.Instance.OpenScreen(UIManager.SCREEN.PAUSE);
    }

    public void SetBackground(Sprite pBackground)
    {
        m_iBackground.sprite = pBackground;
    }

    public Text TMoves
    {
        get
        {
            return m_tMoves;
        }

        set
        {
            m_tMoves = value;
        }
    }

    public Text TScore
    {
        get
        {
            return m_tScore;
        }

        set
        {
            m_tScore = value;
        }
    }

    // set all events when has a match
    public void SetEvents()
    {
        m_pMonsterAnimation.TryReviveMonster(true);
    }



}
