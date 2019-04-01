using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "NewSpecialIcon", menuName = "Match N/ Create Special Icon", order = 1)]
public class SpecialIcon : Icon {
    [HideInInspector]
    [SerializeField]
    private int m_MatchValueToGenerate;

    [HideInInspector]
    [SerializeField]
    private string m_GeneratesTag;

    public int MatchValueToGenerate
    {
        get
        {
            return m_MatchValueToGenerate;
        }

        set
        {
            m_MatchValueToGenerate = value;
        }
    }

    public string GeneratesTag
    {
        get
        {
            return m_GeneratesTag;
        }

        set
        {
            m_GeneratesTag = value;
        }
    }
}
