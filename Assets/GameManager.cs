using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private Board m_pBoard;

    public List<Icon> m_IconToDestroy;

    public bool CheckSurvivors()
    {
        BoardIcon[,] tBoardIcons = PBoard.GetBoardIcons();
        for (int i = 0; i < tBoardIcons.GetLength(0); i++)
        {
            for (int j = 0; j < tBoardIcons.GetLength(1); j++)
            {
                if(tBoardIcons[i,j]!=null)
                {
                    if (tBoardIcons[i, j].STag == "Rescue")
                    {
                        return false;
                    }
                } 
            }
        }
        return true;
    }

    public void ResetBoard()
    {
        GamePlayUI tGamePlayUI = UIManager.Instance.GetScreenObject(UIManager.SCREEN.GAMEPLAY).GetComponent<GamePlayUI>();

        // Reset Board
        //PBoard.moves = 14;

        //PBoard.UpdateMoveScore();

        //set all events to invoke when player do a match
        PBoard.TriggerMatch = new System.Action(tGamePlayUI.SetEvents);
    }

    public static GameManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public Board PBoard
    {
        get
        {
            return m_pBoard;
        }

        set
        {
            m_pBoard = value;
        }
    }
}
