using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu( fileName = "NewIcon", menuName = "Match N/ CreateIcon", order = 0)]
public class Icon : ScriptableObject {
    [System.Serializable]
    public class Action
    {
        
        public BaseAction.ACTION_TYPE m_Type;
    
        public BaseAction m_Action;

        public BaseAction.ACTION_TYPE Type
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

        public BaseAction ActionToRun
        {
            get
            {
                return m_Action;
            }

            set
            {
                m_Action = value;
            }
        }
    }
    [HideInInspector]
    [SerializeField]
    private Sprite m_IconSprite;

    [HideInInspector]
    [SerializeField]
    private string m_Tag;

    [HideInInspector]
    [SerializeField]
    private List<Action> m_Actions;

    public Sprite IconSprite
    {
        get
        {
            return m_IconSprite;
        }

        set
        {
            m_IconSprite = value;
        }
    }

    public List<Action> Actions
    {
        get
        {
            return m_Actions;
        }

        set
        {
            m_Actions = value;
        }
    }

    public string Tag
    {
        get
        {
            return m_Tag;
        }

        set
        {
            m_Tag = value;
        }
    }
}
