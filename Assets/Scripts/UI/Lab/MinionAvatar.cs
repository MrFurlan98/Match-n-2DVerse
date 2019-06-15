using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionAvatar : MonoBehaviour {
    
    public static MinionAvatar instance;

    public Sprite m_headMemberUsing;

    public Sprite m_legMemberUsing;

    public Sprite m_armMemberUsing;

    public Sprite m_bodyMemberUsing;

    private void Awake()
    {
        instance = this;
    }


    public void setMembers(Image m_HeadButton, Image m_LegsButton, Image m_ArmButton, Image m_BodyButton)
    {
        m_headMemberUsing = m_HeadButton.GetComponent<Image>().sprite;
        m_legMemberUsing = m_LegsButton.GetComponent<Image>().sprite;
        m_armMemberUsing = m_ArmButton.GetComponent<Image>().sprite;
        m_bodyMemberUsing = m_BodyButton.GetComponent<Image>().sprite;
    }

}
