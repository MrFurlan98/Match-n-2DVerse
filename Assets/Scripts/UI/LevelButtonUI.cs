using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonUI : MonoBehaviour {

    private int m_IndexButton;

    private Vector2Int m_BoardAspect = new Vector2Int(7, 7);

    public void OpenBoard(int i)
    {

        ScoreManager.Instance.Scenario = BoardManager.Instance.Nivel[i];
        ScoreManager.Instance.Type = BoardManager.Instance.Type[i];
        Debug.Log(BoardManager.Instance.Nivel[i]);
        Debug.Log(BoardManager.Instance.Type[i]);
        GameManager.Instance.PBoard.ClearBoard();
        GameManager.Instance.PBoard.InitBoard(i);

        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.POPUP);

        ScoreManager.Instance.StartGame = false;
        BoardManager.Instance.currentLevel = i;
        ScoreManager.Instance.MovesLeft = 5;
        ScoreManager.Instance.TargetLeft = BoardManager.Instance.GetQtd(i);
        ScoreManager.Instance.NumberToDestroy = ScoreManager.Instance.SetTargets(i);
        ScoreManager.Instance.GoalPoints = 200;
        ScoreManager.Instance.Points = 0;
        
        ScoreManager.Instance.Timer = 30;
        if (BoardManager.Instance.Type[i] == "Desativar_Bomba")
        {
            ScoreManager.Instance.Stop = false;
            ScoreManager.Instance.StartClock();
        }

        UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.GAMEPLAY);
        if(BoardManager.Instance.Type[i] == "Resgate")
        {
            UIManagerBeta.Instance.OpenPanel(UIManagerBeta.PANELS.RESCUE);
        }
        if (BoardManager.Instance.Type[i] == "Desativar_Bomba")
        {
            UIManagerBeta.Instance.OpenPanel(UIManagerBeta.PANELS.DEACTIVATE_BOMB);
        }
        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.ROADMAP);
        ScoreManager.Instance.StartGame = true;

    }
}
