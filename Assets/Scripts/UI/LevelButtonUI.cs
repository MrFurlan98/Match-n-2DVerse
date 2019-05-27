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
        GameManager.Instance.PBoard.ClearBoard();
        GameManager.Instance.PBoard.InitBoard(i);

        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.POPUP);

        ScoreManager.Instance.StartGame = false;
        BoardManager.Instance.currentLevel = i;
        ScoreManager.Instance.MovesLeft = 100;
        ScoreManager.Instance.TargetLeft = BoardManager.Instance.GetQtd(i);
        ScoreManager.Instance.NumberToDestroy = ScoreManager.Instance.SetTargets(i);
        ScoreManager.Instance.GoalPoints = 100;
        ScoreManager.Instance.Points = 0;
        
        ScoreManager.Instance.Timer = 30;
        if (BoardManager.Instance.Type[i] == "Desativar_Bomba")
        {
            ScoreManager.Instance.Stop = false;
            ScoreManager.Instance.StartClock();
        }

        UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.GAMEPLAY);
        GamePlayUI.instance.SetMembers(m_headToSend, m_legToSend, m_armToSend, m_bodyToSend);

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
        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.ROADMAP);
        ScoreManager.Instance.StartGame = true;

    }
}
