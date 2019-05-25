using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonUI : MonoBehaviour {

    private int m_IndexButton;

    private Vector2Int m_BoardAspect = new Vector2Int(7, 7);

    public void OpenBoard(int i)
    {
        ScoreManager.Instance.MovesLeft = 20;
        ScoreManager.Instance.TargetLeft = BoardManager.Instance.GetQtd(i);
        ScoreManager.Instance.IconToDestroy = IconManager.Instance.GetRandomIconSprite();
        ScoreManager.Instance.Points = 0;
        ScoreManager.Instance.Scenario = BoardManager.Instance.Nivel[i];
        ScoreManager.Instance.Type = BoardManager.Instance.Type[i];
        GameManager.Instance.PBoard.ClearBoard();
        GameManager.Instance.PBoard.InitBoard(i);

        UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.GAMEPLAY);
        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.ROADMAP);

    }
}
