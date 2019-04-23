using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonUI : MonoBehaviour {

    private int m_IndexButton;

    private Vector2Int m_BoardAspect = new Vector2Int(7, 7);

    public void OpenBoard()
    {
        ScoreManager.Instance.MovesLeft = 15;
        ScoreManager.Instance.GoalPoints = 200;
        ScoreManager.Instance.Points = 0;
        GameManager.Instance.PBoard.ClearBoard();
        GameManager.Instance.PBoard.InitBoard();

        UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.GAMEPLAY);
        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.BUTTONS.ROADMAP);

    }
}
