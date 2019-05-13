using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager m_Instance;

    [SerializeField]
    private PlayerProfile m_PlayerProfileData = new PlayerProfile();

    private void Awake()
    {
        m_Instance = this;
    }

    public static PlayerManager Instance
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

    public PlayerProfile PlayerProfileData
    {
        get
        {
            return m_PlayerProfileData;
        }

        set
        {
            m_PlayerProfileData = value;
        }
    }
}
