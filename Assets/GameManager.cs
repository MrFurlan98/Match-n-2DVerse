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

    public void ResetBoard()
    {
        GamePlayUI tGamePlayUI = UIManager.Instance.GetScreenObject(UIManager.SCREEN.GAMEPLAY).GetComponent<GamePlayUI>();

        // Reset Board
        m_pBoard.moves = 14;

        m_pBoard.UpdateMoveScore();

        //set all events to invoke when player do a match
        m_pBoard.TriggerMatch = new System.Action(tGamePlayUI.SetEvents);
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
}
