using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "NewBoost", menuName = "Match N/ Create Boost", order = 3)]
public class Boost : ScriptableObject{
    [System.Serializable]
    public class Effect
    {
        public BaseEffect.EFFECT_TYPE type;

        public BaseEffect m_Effect;

        public BaseEffect.EFFECT_TYPE Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public BaseEffect EffectToRun
        {
            get
            {
                return m_Effect;
            }

            set
            {
                m_Effect = value;
            }
        }
    }
    [HideInInspector]
    [SerializeField]
    protected int qtd;

    [HideInInspector]
    [SerializeField]
    protected Sprite boostSprite;

    [HideInInspector]
    [SerializeField]
    protected int valueToBuy;

    [HideInInspector]
    [SerializeField]
    protected float timeLimit;

    [HideInInspector]
    [SerializeField]
    protected string tag;

    [HideInInspector]
    [SerializeField]
    protected List<Effect> effects = new List<Effect>();

    [HideInInspector]
    [SerializeField]
    protected bool isInEffect;

    public int Qtd
    {
        get
        {
            return qtd;
        }

        set
        {
            qtd = value;
        }
    }

    public Sprite BoostSprite
    {
        get
        {
            return boostSprite;
        }

        set
        {
            boostSprite = value;
        }
    }

    public int ValueToBuy
    {
        get
        {
            return valueToBuy;
        }

        set
        {
            valueToBuy = value;
        }
    }

    public float TimeLimit
    {
        get
        {
            return timeLimit;
        }

        set
        {
            timeLimit = value;
        }
    }

    public string Tag
    {
        get
        {
            return tag;
        }

        set
        {
            tag = value;
        }
    }

    public List<Effect> Effects
    {
        get
        {
            return effects;
        }

        set
        {
            effects = value;
        }
    }

    public bool IsInEffect
    {
        get
        {
            return isInEffect;
        }

        set
        {
            isInEffect = value;
        }
    }
}
