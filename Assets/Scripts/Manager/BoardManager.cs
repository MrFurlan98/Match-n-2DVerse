using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    private static BoardManager m_Instance;

    [SerializeField]
    private Board m_pBoard;

    [SerializeField]
    private List<int[,]> m_levels;
    [SerializeField]
    private string[] nivel;

    private string[] m_Type;

    public int currentLevel = 0;

    private void Awake()
    {
        Instance = this;
        string model = File.ReadAllText(Application.dataPath + BoardGenerator.PATH_MODEL);
        Levels = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int[,]>>(model);
        int qtd = Levels.Count;
        nivel = new string[Levels.Count];
        m_Type = new string[Levels.Count];
        int a = 0;
        int b = 0;
        int seed = 1000;
        for (int i = 0; i <  qtd / 2; i++)
        {
            if (b % 2 == 0)
            {
                if (a < 2)
                {
                    nivel[i] = "APOCALIPTICO";
                }
                if (a == 2)
                {
                    nivel[i] = "GREGO";
                }
                if (a > 2)
                {
                    nivel[i] = "APOCALIPTICO";
                    b++;
                }
                a++;
                if (a == 4)
                {
                    a = 0;
                }
            }
            else
            {
                if (a < 2)
                {
                    nivel[i] = "GREGO";
                }
                if (a == 2)
                {
                    nivel[i] = "APOCALIPTICO";
                }
                if (a > 2)
                {
                    nivel[i] = "GREGO";
                    b++;
                }
                a++;
                if (a == 4)
                {
                    a = 0;
                }
            }
        }
        a = 0;
        for (int i = qtd / 2; i < qtd; i++)
        {
            if (a < 2)
            {
                nivel[i] = "APOCALIPTICO";
                a++;
            }
            else
            {
                a++;
                nivel[i] = "GREGO";
                if (a == 4)
                {
                    a = 0;
                }
            }
        }
        System.Random prng = new System.Random(seed);
        for (int i = 0; i < qtd; i++)
        {
            if (nivel[i] == "APOCALIPTICO")
            {
                if (prng.Next(0, 2) == 0)
                {
                    m_Type[i] = "Resgate";
                }
                else
                {
                    m_Type[i] = "Desativar_Bomba";
                }
            }
            else
            {
                if (prng.Next(0, 2) == 0)
                {
                    m_Type[i] = "Um_Dos_Doze_Trabalhos";
                }
                else
                {
                    m_Type[i] = "Sobre_O_Olhar_Da_Gorgona";
                }
            }
        }
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

    public void SetBoardDeactivateBomb(int level,int heigth,int width, BoardIcon[,] tBoardIcons)
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
            Icon tIcon = IconManager.Instance.TransformIcon(i, j, "Modulo");
            if (tIcon != null)
            {
                tBoardIcons[i, j].SetBoardData(tIcon);
                tBoardIcons[i, j].Type = BoardIcon.E_Type.NORMAL;
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

    public string[] Nivel
    {
        get
        {
            return nivel;
        }

        set
        {
            nivel = value;
        }
    }

    public string[] Type
    {
        get
        {
            return m_Type;
        }

        set
        {
            m_Type = value;
        }
    }

    public List<int[,]> Levels
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


    #endregion
}
