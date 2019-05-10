using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsManager : MonoBehaviour {

    public static PartsManager instance;

    private int maxItensToAdd = 5;

    private void Awake()
    {
        instance = this;
        
        armParts.Add(new itenArray(inicialArm));
        bodyParts.Add(new itenArray(inicialBody));
        headParts.Add(new itenArray(inicialHead));
        legParts.Add(new itenArray(inicialLeg));
    }

    public class itenArray
    {
        public MobPart part;
        public int qnt=0;

        public itenArray(MobPart newMobPart)
        {
            part = newMobPart;
            qnt++;
        }
    }

    [SerializeField]
    private MobPart inicialHead;

    [SerializeField]
    private MobPart inicialBody;

    [SerializeField]
    private MobPart inicialLeg;

    [SerializeField]
    private MobPart inicialArm;
        
    public List<itenArray> headParts = new List<itenArray>();

    public List<itenArray> armParts = new List<itenArray>();

    [SerializeField]
    public List<itenArray> legParts = new List<itenArray>();
    
    [SerializeField]
    public List<itenArray> bodyParts = new List<itenArray>();

    public void AddHead(MobPart part)
    {
        for (int i = 0; i < headParts.Count; i++)
        {
            if (headParts[i].part.idMember == part.idMember)
            {
                headParts[i].qnt++;
                return;
            }
        }

        headParts.Add(new itenArray(part));
    }

    public void AddBody(MobPart part)
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (bodyParts[i].part.idMember == part.idMember)
            {
                bodyParts[i].qnt++;
                return;
            }
        }

        bodyParts.Add(new itenArray(part));
    }

    public void AddLeg(MobPart part)
    {
        for (int i = 0; i < legParts.Count; i++)
        {
            if (legParts[i].part.idMember == part.idMember)
            {
                legParts[i].qnt++;
                return;
            }
        }

        legParts.Add(new itenArray(part));
    }

    public void AddArm(MobPart part)
    {
        for (int i = 0; i < armParts.Count; i++)
        {
            if (armParts[i].part.idMember == part.idMember)
            {
                armParts[i].qnt++;
                return;
            }
        }

        armParts.Add(new itenArray(part));
    }

    
    #region Create Array of Sprites

    public Sprite[] CreateBodySpritesArray()
    {
        Sprite[] instPartUsing = new Sprite[bodyParts.Count];
        for (int i = 0; i < (bodyParts.Count); i++)
        {
            instPartUsing[i] = bodyParts[i].part.memberSprite;
        }

        return instPartUsing;
    }

    public Sprite[] CreateLegSpritesArray()
    {
        Sprite[] instPartUsing = new Sprite[legParts.Count];
        for (int i = 0; i < (legParts.Count); i++)
        {
            instPartUsing[i] = legParts[i].part.memberSprite;
        }

        return instPartUsing;
    }

    public Sprite[] CreateHeadSpritesArray()
    {
        Sprite[] instPartUsing = new Sprite[headParts.Count];
        for (int i = 0; i < (headParts.Count); i++)
        {
            instPartUsing[i] = headParts[i].part.memberSprite;
        }

        return instPartUsing;
    }

    public Sprite[] CreateArmSpritesArray()
    {
        Sprite[] instPartUsing = new Sprite[armParts.Count];
        for (int i = 0; i < (armParts.Count); i++)
        {
            instPartUsing[i] = armParts[i].part.memberSprite;
        }

        return instPartUsing;
    }

    #endregion
}
