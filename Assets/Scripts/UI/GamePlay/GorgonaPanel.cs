using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GorgonaPanel : MonoBehaviour {

    [SerializeField]
    private Text m_tScore;

    [SerializeField]
    private Text m_tMoves;

    [SerializeField]
    private Text m_ObjectivePoint;

    [SerializeField]
    private Image m_Sword;

    private void Update()
    {
        m_tScore.text = ScoreManager.Instance.Points.ToString();

        m_tMoves.text = ScoreManager.Instance.MovesLeft.ToString();

        m_ObjectivePoint.text = ScoreManager.Instance.GoalPoints.ToString();

        if (ScoreManager.Instance.StartGame)
        {

            if (ScoreManager.Instance.Points >= ScoreManager.Instance.GoalPoints)
            {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.POPUP);
                PopUpUI.Instance.OpenPopUp("Fim de jogo", true, GamePlayUI.instance.BackMenu);
                ScoreManager.Instance.StartGame = false;
            }
            if (ScoreManager.Instance.MovesLeft <= 0)
            {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.POPUP);
                PopUpUI.Instance.OpenPopUp("Fim de jogo", false, GamePlayUI.instance.BackMenu);
                ScoreManager.Instance.StartGame = false;
            }
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

    public Text ObjectivePoint
    {
        get
        {
            return m_ObjectivePoint;
        }

        set
        {
            m_ObjectivePoint = value;
        }
    }

    public Image Sword
    {
        get
        {
            return m_Sword;
        }

        set
        {
            m_Sword = value;
        }
    }
}
