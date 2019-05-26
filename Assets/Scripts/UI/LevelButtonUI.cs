using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour {

    private int m_IndexButton;

    #region Sprites to Send
    private Image headMember;
    private Image legMember;
    private Image armMember;
    private Image bodyMember;
    #endregion
    #region Instances
    private RoadMapUI instanceRM;
    private GamePlayUI instanceGP;
    #endregion

    private Vector2Int m_BoardAspect = new Vector2Int(7, 7);

    public void OpenBoard()
    {
        ScoreManager.Instance.MovesLeft = 1;
        ScoreManager.Instance.GoalPoints = 2;
        ScoreManager.Instance.Points = 0;
        GameManager.Instance.PBoard.ClearBoard();
        GameManager.Instance.PBoard.InitBoard();

        UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.GAMEPLAY);
        #region Reference Instances
        instanceRM = RoadMapUI.instance;
        instanceGP = GamePlayUI.instance;
        #endregion
        #region setAvatarGameplay
        headMember = instanceRM.m_HeadRoadAvatar;
        armMember = instanceRM.m_ArmRoadAvatar;
        legMember = instanceRM.m_LegRoadAvatar;
        bodyMember = instanceRM.m_BodyRoadAvatar;
        instanceGP.SetMembers(headMember, legMember, armMember, bodyMember);
        #endregion
        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.ROADMAP);

    }
}
