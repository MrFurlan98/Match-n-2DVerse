using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAvatar : MonoBehaviour {

    MinionAvatar instanceMA;

    [SerializeField]
    private Image m_thisHeadMember;

    [SerializeField]
    private Image m_thisBodyMember;

    [SerializeField]
    private Image m_thisLegMember;

    [SerializeField]
    private Image m_thisArmMember;

    void Start()
    {
        instanceMA = MinionAvatar.instance;
    }

    // Update is called once per frame
    void Update () {
        m_thisArmMember.sprite = instanceMA.m_armMemberUsing;
        m_thisBodyMember.sprite = instanceMA.m_bodyMemberUsing;
        m_thisHeadMember.sprite = instanceMA.m_headMemberUsing;
        m_thisLegMember.sprite = instanceMA.m_legMemberUsing;
    }
}
