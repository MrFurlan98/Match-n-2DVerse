using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TwelveWorksPanel : MonoBehaviour {

    [SerializeField]
    private Text m_tScore;

    [SerializeField]
    private Text m_tMoves;

    [SerializeField]
    private Text m_tModulos;


    private void Update()
    {
        m_tModulos.text = ScoreManager.Instance.TargetLeft.ToString();

        m_tScore.text = ScoreManager.Instance.Points.ToString();

        m_tMoves.text = ScoreManager.Instance.MovesLeft.ToString();

        if (ScoreManager.Instance.StartGame)
        {

            if (ScoreManager.Instance.TargetLeft<=0)
            {
                
                PopUpUI.Instance.OpenPopUp("Fim de jogo", true, GamePlayUI.instance.BackMenu);
                ScoreManager.Instance.StartGame = false;
            }
            if (ScoreManager.Instance.MovesLeft <= 0)
            {
                
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
}
