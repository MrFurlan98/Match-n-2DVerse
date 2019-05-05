using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEffect : IBoostEffect {


    public enum EFFECT_TYPE
    {
        CROSS,
        BLOCK,
        DESTROY_BY_TYPE,
    }

    public BaseEffect()
    {
    }

    public BaseEffect(string targetTag)
    {
        TargetTag = targetTag;
    }

    public static BaseEffect GetEffectObject(EFFECT_TYPE pEffectType)
    {
        switch (pEffectType)
        {
            case EFFECT_TYPE.BLOCK:
                return new BoostExplosion();
            case EFFECT_TYPE.DESTROY_BY_TYPE:
                return new BoostSuper();
            case EFFECT_TYPE.CROSS:
                return new BoostCross();
        }
        return new BaseEffect();
    }

    public static BaseEffect GetEffectObject(EFFECT_TYPE pEffectType, BaseEffect pBaseEffect)
    {
        switch (pEffectType)
        {
            case EFFECT_TYPE.BLOCK:
                return new BoostExplosion(pBaseEffect.m_TargetTag);
            case EFFECT_TYPE.DESTROY_BY_TYPE:
                return new BoostSuper(pBaseEffect.m_TargetTag);
            case EFFECT_TYPE.CROSS:
                return new BoostCross(pBaseEffect.m_TargetTag);
        }
        return new BaseEffect();
    }


    public virtual void Effect(Board pBoard)
    {
        Debug.LogError("Not Implemented Action");
    }
    [HideInInspector]
    [SerializeField]
    private string m_TargetTag;

    public string TargetTag
    {
        get
        {
            return m_TargetTag;
        }

        set
        {
            m_TargetTag = value;
        }
    }
}
