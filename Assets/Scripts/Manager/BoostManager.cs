using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManager : MonoBehaviour {

    private static BoostManager m_Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private Board m_pBoard;

    [SerializeField]
    private List<Boost> m_Boosts;

    [SerializeField]
    private List<float> timeLimits;

    private void Start()
    {
        for(int i=0;i<m_Boosts.Count;i++)
        {
            TimeLimits[i] = 0;
        }
    }
    void Update () {
		if(IsBoostOn())
        {
            for (int i = 0; i < m_Boosts.Count; i++)
            {
                TimeLimits[i] -= Time.deltaTime;
            }
        }
	}

    public void StarBoostEffect(string Tag)
    {
        for(int i=0;i<m_Boosts.Count;i++)
        {
            if(Tag==m_Boosts[i].Tag)
            {
                if(m_Boosts[i].Qtd>0)
                {
                    BaseEffect tBaseEffect = BaseEffect.GetEffectObject(m_Boosts[i].Effects[0].Type);
                    tBaseEffect.Effect(m_pBoard);
                    StartClock(m_Boosts[i],i);
                }
            }
        }
    }
    public void StartClock(Boost pBoost,int i)
    {
        TimeLimits[i] = pBoost.TimeLimit;
    }
    public bool IsBoostOn()
    {
        for (int i = 0; i < m_Boosts.Count; i++)
        {
            if(m_Boosts[i].IsInEffect)
            {
                return true;
            }
        }
        return false;
    }
    public void ApplyEffects()
    {
        for(int i =0;i<m_Boosts.Count;i++)
        {
            if (m_Boosts[i].IsInEffect)
            {
                
            }
        }
    }
    public static BoostManager Instance
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

    public List<Boost> Boosts
    {
        get
        {
            return m_Boosts;
        }

        set
        {
            m_Boosts = value;
        }
    }

    public List<float> TimeLimits
    {
        get
        {
            return timeLimits;
        }

        set
        {
            timeLimits = value;
        }
    }
}
