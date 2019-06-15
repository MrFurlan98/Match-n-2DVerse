using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RescuePanel : MonoBehaviour {

    [SerializeField]
    private Text m_tScore;

    [SerializeField]
    private Text m_tMoves;

    [SerializeField]
    private Text m_tModulos;

    [SerializeField]
    private Text m_ObjectivePoint;


    private void Update()
    {
        m_tModulos.text = ScoreManager.Instance.TargetLeft.ToString();

        m_tScore.text = ScoreManager.Instance.Points.ToString();

        m_tMoves.text = ScoreManager.Instance.MovesLeft.ToString();

        m_ObjectivePoint.text = ScoreManager.Instance.GoalPoints.ToString();

        if (ScoreManager.Instance.StartGame)
        {

            if (GameManager.Instance.CheckSurvivors())
            {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.POPUP);
                PopUpUI.Instance.OpenPopUp("Fim de jogo", true, GamePlayUI.instance.BackMenu);
                ScoreManager.Instance.StartGame = false;
            }
            if (ScoreManager.Instance.MovesLeft <= 0)
            {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.POPUP);
                Debug.Log(ScoreManager.Instance.MovesLeft);
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

    public Text TModulos
    {
        get
        {
            return m_tModulos;
        }

        set
        {
            m_tModulos = value;
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
}
