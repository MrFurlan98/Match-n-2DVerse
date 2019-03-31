using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour {

    private static IconManager m_Instance;

    [SerializeField]
    private List<Icon> m_Icons;

    [SerializeField]
    private List<SpecialIcon> m_SpecialIcons;


    private void Awake()
    {
        Instance = this;
    }


    #region Methods

    public BoardIcon InitBoardIcon(ref GameObject pIconGameObject, int pX, int pY)
    {
        //set position

        pIconGameObject.transform.position = new Vector2(pX, pY);


        //set sprite renderer configure
        SpriteRenderer tSpriteRenderer = pIconGameObject.AddComponent<SpriteRenderer>();

        BoardIcon tBoardIcon = pIconGameObject.AddComponent<BoardIcon>();

        tBoardIcon.SpriteRenderer = tSpriteRenderer;


        // Set row // column
        tBoardIcon.row = pX;
        tBoardIcon.colunm = pY;

        return tBoardIcon;
    }

    public BoardIcon GenerateRandomIcon(int pX, int pY)
    {
        int tRandoIndex = UnityEngine.Random.Range(0, m_Icons.Count);

        Icon tIcon = m_Icons[tRandoIndex];

        GameObject tNewIcon = new GameObject(tIcon.Tag);

        BoardIcon tBoardIcon = InitBoardIcon(ref tNewIcon, pX, pY);

        tBoardIcon.SetBoardData(tIcon);

        return tBoardIcon;
    }

    public BoardIcon GenerateIcon(int pX, int pY, Icon pIconData)
    {
        GameObject tNewIcon = new GameObject(pIconData.Tag);

        BoardIcon tBoardIcon = InitBoardIcon(ref tNewIcon, pX, pY);

        tBoardIcon.SetBoardData(pIconData);

        return tBoardIcon;
    }

    public BoardIcon GenerateSpecialIconByMatch(int pX, int pY, int pAmount, string pGeneratesTag)
    {
        for (int i = 0; i < m_SpecialIcons.Count; i++)
        {
            if (pAmount == m_SpecialIcons[i].MatchValueToGenerate && pGeneratesTag == m_SpecialIcons[i].GeneratesTag)
            {
                BoardIcon tBoardIcon = Instance.GenerateIcon(pX, pY, m_SpecialIcons[i]);
                tBoardIcon.Type = BoardIcon.E_Type.SPECIAL;
                return tBoardIcon;
            }
        }

        return null;
    }

    #endregion

    public static IconManager Instance
    {
        get
        {
            return m_Instance;
        }

        set
        {
            m_Instance = value;
        }
    }

}
