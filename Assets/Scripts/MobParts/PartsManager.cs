﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsManager : MonoBehaviour {

    public static PartsManager instance;

    private int maxItensToAdd = 5;

    private void Awake()
    {
        instance = this;
    }

    public List<MobPart> headParts = new List<MobPart>();
    public List<MobPart> armParts = new List<MobPart>();
    public List<MobPart> legParts = new List<MobPart>();
    public List<MobPart> bodyParts = new List<MobPart>();

    public void AddHead(MobPart part)
    {
        if(headParts.Count < maxItensToAdd)
        {

            headParts.Add(part);

        }
    }

    public void AddBody(MobPart part)
    {
        if (bodyParts.Count < maxItensToAdd)
        {

            bodyParts.Add(part);

        }
    }

    public void AddLeg(MobPart part)
    {
       if (legParts.Count < maxItensToAdd)
       {

            legParts.Add(part);

        }
    }

    public void AddArm(MobPart part)
    {
        if (armParts.Count < maxItensToAdd)
        {

            armParts.Add(part);

        }
    }

    #region Create Array Func To Lab

    public Sprite[] CreateBodySpritesArray()
    {
        Sprite[] instPartUsing = new Sprite[bodyParts.Count];
        for (int i = 0; i < (bodyParts.Count); i++)
        {
            instPartUsing[i] = bodyParts[i].memberSprite;
        }

        return instPartUsing;
    }

    public Sprite[] CreateLegSpritesArray()
    {
        Sprite[] instPartUsing = new Sprite[legParts.Count];
        for (int i = 0; i < (legParts.Count); i++)
        {
            instPartUsing[i] = legParts[i].memberSprite;
        }

        return instPartUsing;
    }

    public Sprite[] CreateHeadSpritesArray()
    {
        Sprite[] instPartUsing = new Sprite[headParts.Count];
        for (int i = 0; i < (headParts.Count); i++)
        {
            instPartUsing[i] = headParts[i].memberSprite;
        }

        return instPartUsing;
    }

    public Sprite[] CreateArmSpritesArray()
    {
        Sprite[] instPartUsing = new Sprite[armParts.Count];
        for (int i = 0; i < (armParts.Count); i++)
        {
            instPartUsing[i] = armParts[i].memberSprite;
        }

        return instPartUsing;
    }

    #endregion
}
