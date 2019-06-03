﻿using System;
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

        ScoreManager.Instance.Scenario = BoardManager.Instance.Levels[i].Scenario;
        ScoreManager.Instance.Type = BoardManager.Instance.Levels[i].Type;
        Debug.Log(BoardManager.Instance.Levels[i].Scenario);
        Debug.Log(BoardManager.Instance.Levels[i].Type);
        

        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.POPUP);

        ScoreManager.Instance.StartGame = false;
        BoardManager.Instance.CurrentLevel = i;
        ScoreManager.Instance.MovesLeft = BoardManager.Instance.Levels[i].MovesLeft;
        ScoreManager.Instance.TargetLeft = BoardManager.Instance.Levels[i].TargetLeft;
        ScoreManager.Instance.GoalPoints = BoardManager.Instance.Levels[i].GoalPoints;
        ScoreManager.Instance.Points = 0;

        GameManager.Instance.PBoard.ClearBoard();
        GameManager.Instance.PBoard.InitBoard(i);

        ScoreManager.Instance.Timer = BoardManager.Instance.Levels[i].Timer;

        if (BoardManager.Instance.Levels[i].Type == "Desativar_Bomba")
        {
            ScoreManager.Instance.SetTargets(ScoreManager.Instance.TargetLeft);
            ScoreManager.Instance.Stop = false;
            ScoreManager.Instance.StartClock();
        }

        UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.GAMEPLAY);

        GamePlayUI.instance.SetMembers(m_headToSend, m_legToSend, m_armToSend, m_bodyToSend);
        GamePlayUI.instance.SetBackground();
        //GamePlayUI.instance.SetBackground(BoardManager.Instance.Nivel[i]);

        if (BoardManager.Instance.Levels[i].Type == "Resgate")
        {
            GamePlayUI.instance.OpenPanel(GamePlayUI.PANELS.RESCUE);
        }
        if (BoardManager.Instance.Levels[i].Type == "Desativar_Bomba")
        {
            GamePlayUI.instance.OpenPanel(GamePlayUI.PANELS.DEACTIVATE_BOMB);
        }
        if (BoardManager.Instance.Levels[i].Type == "Sobre_O_Olhar_Da_Gorgona")
        {
            GamePlayUI.instance.OpenPanel(GamePlayUI.PANELS.UNDER_THE_GORGONAS_EYES);
        }
        if (BoardManager.Instance.Levels[i].Type == "Um_Dos_Doze_Trabalhos")
        {
            GamePlayUI.instance.OpenPanel(GamePlayUI.PANELS.ONE_OF_TWELVE_WORKS);
        }
        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.ROADMAP);
        ScoreManager.Instance.StartGame = true;
        GameManager.Instance.PBoard.StartCoroutine(GameManager.Instance.PBoard.StartDalay());
    }
}
