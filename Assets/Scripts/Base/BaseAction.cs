using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAction : IBoardAction{

    public enum ACTION_TYPE
    {
        DESTROY_CROSS,
        DESTROY_BLOCK,
        DESTROY_BY_TYPE,
        DESTROY_ALL_BOARD,
        DESTROY_DIAGONAL,
        TRANSFORM_INTO_SPECIAL,
        RESCUE_SURVIVOR
    }

    public BaseAction()
    {
    }

    public BaseAction(string targetTag,string tagToTransformTo, int multiplier, Vector2Int nxN)
    {
        TargetTag = targetTag;
        TagToTransformTo = tagToTransformTo;
        Multiplier = multiplier;
        NxN = nxN;
    }

    public static BaseAction GetActionObject(ACTION_TYPE pActionType)
    {
       switch(pActionType)
        {
            case ACTION_TYPE.DESTROY_BLOCK:
                return new DestroyBlock();
            case ACTION_TYPE.DESTROY_BY_TYPE:
                return new DestroyByTag();
            case ACTION_TYPE.DESTROY_ALL_BOARD:
                return new DestroyAllBoard();
            case ACTION_TYPE.DESTROY_CROSS:
                return new DestroyCross();
            case ACTION_TYPE.DESTROY_DIAGONAL:
                return new DestroyDiagonal();
            case ACTION_TYPE.TRANSFORM_INTO_SPECIAL:
                return new TransformIntoSpecial();
        }
        return new BaseAction();
    }

    public static BaseAction GetActionObject(ACTION_TYPE pActionType, BaseAction pBaseAction)
    {
        switch (pActionType)
        {
            case ACTION_TYPE.DESTROY_BLOCK:
                return new DestroyBlock(pBaseAction.m_NxN);
            case ACTION_TYPE.DESTROY_BY_TYPE:
                return new DestroyByTag(pBaseAction.m_TargetTag);
            case ACTION_TYPE.DESTROY_ALL_BOARD:
                return new DestroyAllBoard();
            case ACTION_TYPE.DESTROY_CROSS:
                return new DestroyCross(pBaseAction.m_NxN);
            case ACTION_TYPE.DESTROY_DIAGONAL:
                return new DestroyDiagonal();
            case ACTION_TYPE.TRANSFORM_INTO_SPECIAL:
                return new TransformIntoSpecial(pBaseAction.m_TargetTag,pBaseAction.m_TagToTransformTo);
        }
        return new BaseAction();
    }


    public virtual void Action(int pOriginX, int pOriginY, BoardIcon[,] pIcons)
    {
        Debug.LogError("Not Implemented Action");
    }


    // generic attributes to use in actions
    #region Usage Variables
    [HideInInspector]
    [SerializeField]
    protected string m_TargetTag;

    [HideInInspector]
    [SerializeField]
    protected int m_Multiplier;

    [HideInInspector]
    [SerializeField]
    protected Vector2Int m_NxN;

    [HideInInspector]
    [SerializeField]
    protected string m_TagToTransformTo;

    #endregion

    #region Public Methods

    public virtual string TargetTag
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

    public virtual string TagToTransformTo
    {
        get
        {
            return m_TagToTransformTo;
        }

        set
        {
            m_TagToTransformTo = value;
        }
    }

    public virtual int Multiplier
    {
        get
        {
            return m_Multiplier;
        }

        set
        {
            m_Multiplier = value;
        }
    }

    public virtual Vector2Int NxN
    {
        get
        {
            return m_NxN;
        }

        set
        {
            m_NxN = value;
        }
    }



    #endregion
}
