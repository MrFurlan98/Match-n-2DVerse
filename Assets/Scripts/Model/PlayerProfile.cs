using UnityEngine;

[System.Serializable]
public class PlayerProfile
{

    [Header("User Info")]
    [SerializeField]
    private string m_UserName;

    public string UserName
    {
        get
        {
            return m_UserName;
        }

        set
        {
            m_UserName = value;
        }
    }

}
