using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelButtonUI : MonoBehaviour {

    RoadMapUI instanceRM;

    private Image m_headToSend;
    private Image m_armToSend;
    private Image m_legToSend;
    private Image m_bodyToSend;

    private int m_IndexButton;

    private Vector2Int m_BoardAspect = new Vector2Int(7, 7);

    public void OpenBoard(int i)
    {
        #region SET AVATAR TO SEND
        instanceRM = RoadMapUI.instance;
        m_headToSend = instanceRM.m_HeadRoadAvatar;
        m_legToSend = instanceRM.m_LegRoadAvatar;
        m_bodyToSend = instanceRM.m_BodyRoadAvatar;
        m_armToSend = instanceRM.m_ArmRoadAvatar;
        #endregion

        ScoreManager.Instance.Scenario = BoardManager.Instance.Nivel[i];
        ScoreManager.Instance.Type = BoardManager.Instance.Type[i];
        Debug.Log(BoardManager.Instance.Nivel[i]);
        Debug.Log(BoardManager.Instance.Type[i]);
        

        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.POPUP);

        ScoreManager.Instance.StartGame = false;
        BoardManager.Instance.CurrentLevel = i;
        ScoreManager.Instance.MovesLeft = 100;
        ScoreManager.Instance.TargetLeft = BoardManager.Instance.GetQtd(i);
        ScoreManager.Instance.NumberToDestroy = ScoreManager.Instance.SetTargets(i);
        ScoreManager.Instance.GoalPoints = 100;
        ScoreManager.Instance.Points = 0;

        GameManager.Instance.PBoard.ClearBoard();
        GameManager.Instance.PBoard.InitBoard(i);

        ScoreManager.Instance.Timer = 30;
        if (BoardManager.Instance.Type[i] == "Desativar_Bomba")
        {
            ScoreManager.Instance.Stop = false;
            ScoreManager.Instance.StartClock();
        }

        UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.GAMEPLAY);

        GamePlayUI.instance.SetMembers(m_headToSend, m_legToSend, m_armToSend, m_bodyToSend);
        GamePlayUI.instance.SetBackground();
        //GamePlayUI.instance.SetBackground(BoardManager.Instance.Nivel[i]);

        if (BoardManager.Instance.Type[i] == "Resgate")
        {
            UIManagerBeta.Instance.OpenPanel(UIManagerBeta.PANELS.RESCUE);
        }
        if (BoardManager.Instance.Type[i] == "Desativar_Bomba")
        {
            UIManagerBeta.Instance.OpenPanel(UIManagerBeta.PANELS.DEACTIVATE_BOMB);
        }
        if (BoardManager.Instance.Type[i] == "Sobre_O_Olhar_Da_Gorgona")
        {
            UIManagerBeta.Instance.OpenPanel(UIManagerBeta.PANELS.UNDER_THE_GORGONAS_EYES);
        }
        if (BoardManager.Instance.Type[i] == "Um_Dos_Doze_Trabalhos")
        {
            UIManagerBeta.Instance.OpenPanel(UIManagerBeta.PANELS.ONE_OF_TWELVE_WORKS);
        }
        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.ROADMAP);
        ScoreManager.Instance.StartGame = true;

    }
}
