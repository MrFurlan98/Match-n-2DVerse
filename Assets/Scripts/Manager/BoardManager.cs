using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    private static BoardManager m_Instance;

    [SerializeField]
    private Board m_pBoard;

    [SerializeField]
    private List<Level> m_levels;

    private int currentLevel;

    private void Awake()
    {
        Instance = this;
        string model = Resources.Load<TextAsset>(BoardGenerator.PATH_MODEL).text;
        Levels = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Level>>(model);
    }

    #region Methods

    public void SetBoardRescue(int level,int heigth,int width, BoardIcon[,] tBoardIcons)
    {
        int qtd = GetQtd(level);
        for (int z = 0; z < qtd; z++)
        {
            int i = 0, j = 0;
            do
            {
                i = UnityEngine.Random.Range(0, tBoardIcons.GetLength(0));
                j = UnityEngine.Random.Range(0, tBoardIcons.GetLength(1));
            } while (tBoardIcons[i, j].Type == BoardIcon.E_Type.SPECIAL || tBoardIcons[i, j].StateIcon == BoardIcon.E_State.CANT_DESTROY);
            Icon tIcon = IconManager.Instance.TransformIcon(i, j, "Rescue");
            if (tIcon != null)
            {
                tBoardIcons[i, j].SetBoardData(tIcon);
                tBoardIcons[i, j].Type = BoardIcon.E_Type.NORMAL;
            }
        }
    }
    public void SetBoardWorks(int level, int heigth, int width, BoardIcon[,] tBoardIcons)
    {
        int qtd = GetQtd(level);
        for (int i = 0; i < qtd; i++)
        {
            int j = 0;
            do
            {
                j = UnityEngine.Random.Range(0, tBoardIcons.GetLength(0));
            } while (tBoardIcons[j, heigth-1].Type == BoardIcon.E_Type.SPECIAL || tBoardIcons[j, heigth-1].StateIcon == BoardIcon.E_State.CANT_DESTROY || tBoardIcons[j, heigth - 1].StateIcon == BoardIcon.E_State.OBJECTIVE);
            Icon tIcon = IconManager.Instance.TransformIcon(heigth-1, j, "Down");
            if (tIcon != null)
            {
                tBoardIcons[j, heigth-1].SetBoardData(tIcon);
                tBoardIcons[j, heigth-1].Type = BoardIcon.E_Type.NORMAL;
                tBoardIcons[j, heigth - 1].StateIcon = BoardIcon.E_State.OBJECTIVE;
            }
        }
    }

    public int GetQtd(int level)
    {
        if (level <= m_levels.Count / 5)
        {
            return 1;
        }
        if (level <= m_levels.Count * 2 / 5 && level > m_levels.Count / 5)
        {
            return 2;
        }
        if (level <= m_levels.Count * 3 / 5 && level > m_levels.Count * 2 / 5)
        {
            return 3;
        }
        if (level <= m_levels.Count * 4 / 5 && level > m_levels.Count * 3 / 5)
        {
            return 4;
        }
        if (level <= m_levels.Count && level > m_levels.Count * 4 / 5)
        {
            return 5;
        }
        return 6;
    }
    public static BoardManager Instance
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

    public List<Level> Levels
    {
        get
        {
            return m_levels;
        }

        set
        {
            m_levels = value;
        }
    }


    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }

        set
        {
            currentLevel = value;
        }
    }


    #endregion
}
