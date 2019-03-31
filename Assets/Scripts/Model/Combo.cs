using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "NewCombo", menuName = "Match N/ Create Combo", order = 2)]
public class Combo : ScriptableObject {
    [HideInInspector]
    [SerializeField]
    private List<Icon.Action> m_Actions = new List<Icon.Action>();

    [HideInInspector]
    [SerializeField]
    private SpecialIcon m_Icon1;

    [HideInInspector]
    [SerializeField]
    private SpecialIcon m_Icon2;

    public List<Icon.Action> Actions
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

    public SpecialIcon Icon1
    {
        get
        {
            return m_Icon1;
        }

        set
        {
            m_Icon1 = value;
        }
    }

    public SpecialIcon Icon2
    {
        get
        {
            return m_Icon2;
        }

        set
        {
            m_Icon2 = value;
        }
    }


}
