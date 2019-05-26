using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsManager : MonoBehaviour {

    public static PartsManager instance;

    private int maxItensToAdd = 5;

    private void Awake()
    {
        instance = this;

        /*armParts.Add(new itenArray(inicialArm));
        bodyParts.Add(new itenArray(inicialBody));
        headParts.Add(new itenArray(inicialHead));
        legParts.Add(new itenArray(inicialLeg));*/

        armParts.Add(inicialArm);
        bodyParts.Add(inicialBody);
        headParts.Add(inicialHead);
        legParts.Add(inicialLeg);
    }

    /*public class itenArray
    {
        public MobPart part;
        public int qnt=0;

        public itenArray(MobPart newMobPart)
        {
            part = newMobPart;
            qnt++;
        }
    }*/

    [SerializeField]
    private MobPart inicialHead;

    [SerializeField]
    private MobPart inicialBody;

    [SerializeField]
    private MobPart inicialLeg;

    [SerializeField]
    private MobPart inicialArm;    
    
    public List<MobPart> headParts = new List<MobPart>();

    public List<MobPart> armParts = new List<MobPart>();

    [SerializeField]
    public List<MobPart> legParts = new List<MobPart>();
    
    [SerializeField]
    public List<MobPart> bodyParts = new List<MobPart>();

    /*
    public void AddHead(MobPart part)
    {
        for (int i = 0; i < headParts.Count; i++)
        {
            if (headParts[i].idMember == part.idMember)
            {
                // headParts[i].qnt++;
                hardCurrency++;
                return;
            }
        }

        //headParts.Add(new itenArray(part));
        headParts.Add(part);
    }

    public void AddBody(MobPart part)
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (bodyParts[i].idMember == part.idMember)
            {
                //bodyParts[i].qnt++;
                hardCurrency++;
                return;
            }
        }

        //bodyParts.Add(new itenArray(part));
        bodyParts.Add(part);
    }

    public void AddLeg(MobPart part)
    {

    }

    public void AddArm(MobPart part)
    {
        for (int i = 0; i < armParts.Count; i++)
        {
            if (armParts[i].idMember == part.idMember)
            {
                //armParts[i].qnt++;
                hardCurrency++;
                return;
            }
        }

        //armParts.Add(new itenArray(part));
        armParts.Add(part);
    }*/    
    #region Create Array of Sprites

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
