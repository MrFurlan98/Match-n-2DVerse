using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour {

    private static IconManager m_Instance;

    [SerializeField]
    private GameObject m_PrefabBaseIconSetting;

    [SerializeField]
    private List<Icon> m_Icons;

    [SerializeField]
    private List<SpecialIcon> m_SpecialIcons;

    [SerializeField]
    private List<Combo> m_Combos;

    [SerializeField]
    private Transform m_BoardRoot;

    [SerializeField]
    private List<Sprite> m_Modulos = new List<Sprite>();

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
        SpriteRenderer tSpriteRenderer = pIconGameObject.GetComponent<SpriteRenderer>();

        BoardIcon tBoardIcon = pIconGameObject.GetComponent<BoardIcon>();

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

        GameObject tNewIcon = Instantiate(m_PrefabBaseIconSetting, m_BoardRoot);

        tNewIcon.name = tIcon.Tag;

        BoardIcon tBoardIcon = InitBoardIcon(ref tNewIcon, pX, pY);

        tBoardIcon.SetBoardData(tIcon);

        return tBoardIcon;
    }

    public BoardIcon GenerateIcon(int pX, int pY, Icon pIconData)
    {
        GameObject tNewIcon = Instantiate(m_PrefabBaseIconSetting, m_BoardRoot);

        tNewIcon.name = pIconData.Tag;

        BoardIcon tBoardIcon = InitBoardIcon(ref tNewIcon, pX, pY);

        tBoardIcon.SetBoardData(pIconData);

        return tBoardIcon;
    }
    public BoardIcon GenerateIndestructableIcon(int pX, int pY)
    {
        GameObject tNewIcon = Instantiate(m_PrefabBaseIconSetting, m_BoardRoot);

        tNewIcon.name = "Indestructable";

        BoardIcon tBoardIcon = InitBoardIcon(ref tNewIcon, pX, pY);

        tBoardIcon.SetBoardData(new Icon());

        tBoardIcon.StateIcon = BoardIcon.E_State.CANT_DESTROY;

        return tBoardIcon;
    }
    public BoardIcon GenerateRescueIcon(int pX, int pY)
    {
        GameObject tNewIcon = Instantiate(m_PrefabBaseIconSetting, m_BoardRoot);

        tNewIcon.name = "Rescue";

        BoardIcon tBoardIcon = InitBoardIcon(ref tNewIcon, pX, pY);

        tBoardIcon.SetBoardData(new Icon());

        tBoardIcon.StateIcon = BoardIcon.E_State.STAND_BY;

        return tBoardIcon;
    }
    public void TransformEffect(string tag,int heigth,int width,BoardIcon[,] tBoardIcons)
    {
        int i = 0, j = 0;
        do
        {
            i = UnityEngine.Random.Range(0, tBoardIcons.GetLength(0));
            j = UnityEngine.Random.Range(0, tBoardIcons.GetLength(1));
        } while (tBoardIcons[i, j].Type == BoardIcon.E_Type.SPECIAL);
        if (tag == "Super")
        {
            tag = GetTagSuperIcon();
        }
        Icon tIcon = TransformIcon(i, j, tag);
        if (tIcon != null)
        {
            tBoardIcons[i, j].SetBoardData(tIcon);
            tBoardIcons[i, j].Type = BoardIcon.E_Type.SPECIAL;
        }
        
    }

    public List<Sprite> ShambleIconSprite()
    {
        for (int i = 0; i < m_Modulos.Count; i++)
        {
            int j = UnityEngine.Random.Range(0, m_Modulos.Count);
            Sprite tSprite = m_Modulos[i];
            m_Modulos[i] = m_Modulos[j];
            m_Modulos[j] = tSprite;
        }
        
        return m_Modulos;
    }

    public List<Icon> GetModulos(List<Sprite> tSprite)
    {
        List<Icon> tIcons = new List<Icon>();
        
        for (int i = 0; i < tSprite.Count; i++)
        {
            for (int j = 0; j < m_SpecialIcons.Count; j++)
            {
                if (m_SpecialIcons[j].IconSprite == tSprite[i])
                {
                    tIcons.Add(m_SpecialIcons[j]);
                }
            }
            
        }
        return tIcons;
    }

    public Icon TransformIcon(int pX,int pY,string pTagToTransformTo)
    {
        for (int i = 0; i < m_SpecialIcons.Count; i++)
        {
            if (m_SpecialIcons[i].Tag == pTagToTransformTo)
            {
                 return m_SpecialIcons[i];
            }
        }
       
        return null;
    }

    public BoardIcon GenerateSpecialIconByMatch(int pX, int pY, int pAmount, string pGeneratesTag)
    {
        for (int i = 0; i < m_SpecialIcons.Count; i++)
        {
            if(pAmount>=8)
            {
                if (pGeneratesTag == m_SpecialIcons[i].GeneratesTag)
                {
                    BoardIcon tBoardIcon = Instance.GenerateIcon(pX, pY, m_SpecialIcons[i]);
                    tBoardIcon.Type = BoardIcon.E_Type.SPECIAL;
                    return tBoardIcon;
                }
            }
            if(m_SpecialIcons[i].GeneratesTag == "ALL")
            {
                if (pAmount == m_SpecialIcons[i].MatchValueToGenerate)
                {
                    BoardIcon tBoardIcon = Instance.GenerateIcon(pX, pY, m_SpecialIcons[i]);
                    tBoardIcon.Type = BoardIcon.E_Type.SPECIAL;
                    return tBoardIcon;
                }
            }
            if (pAmount == m_SpecialIcons[i].MatchValueToGenerate && pGeneratesTag == m_SpecialIcons[i].GeneratesTag)
            {
                BoardIcon tBoardIcon = Instance.GenerateIcon(pX, pY, m_SpecialIcons[i]);
                tBoardIcon.Type = BoardIcon.E_Type.SPECIAL;
                return tBoardIcon;
            }
        }

        return null;
    }
    public Icon GetIcon(string tag)
    {
        for (int i = 0; i < m_Icons.Count; i++)
        {
            if(tag == m_Icons[i].Tag)
            {
                return m_Icons[i];
            }
        }
        return null;
    }
    public string GetTagSuperIcon()
    {
        int i = UnityEngine.Random.Range(0, 4);
        return m_SpecialIcons[i].Tag;
    }

    public SpecialIcon GetSpecialIcon(BoardIcon pIcon)
    {
        for (int i = 0; i < m_SpecialIcons.Count; i++)
        {
            if (pIcon.STag == m_SpecialIcons[i].Tag)
            {
                return m_SpecialIcons[i];
            }
        }
        return null;
    }
    public Combo GetCombo(BoardIcon pIcon1, BoardIcon pIcon2)
    {
        for (int i = 0; i < m_Combos.Count; i++)
        {
            if ((m_Combos[i].Icon1.Tag == pIcon1.STag && m_Combos[i].Icon2.Tag == pIcon2.STag) || (m_Combos[i].Icon2.Tag == pIcon1.STag && m_Combos[i].Icon1.Tag == pIcon2.STag))
                return m_Combos[i];
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

    public List<Combo> Combos
    {
        get
        {
            return m_Combos;
        }

        set
        {
            m_Combos = value;
        }
    }

    public List<Sprite> Modulos
    {
        get
        {
            return m_Modulos;
        }

        set
        {
            m_Modulos = value;
        }
    }
}
