using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Icon> m_IconToDestroy;

    public bool CheckSurvivors()
    {
        BoardIcon[,] tBoardIcons = GamePlayManager.Instance.BoardReference.GetBoardIcons();
        for (int i = 0; i < tBoardIcons.GetLength(0); i++)
        {
            for (int j = 0; j < tBoardIcons.GetLength(1); j++)
            {
                if(tBoardIcons[i,j]!=null)
                {
                    if (tBoardIcons[i, j].STag == "Rescue")
                    {
                        return false;
                    }
                } 
            }
        }
        return true;
    }

    public static GameManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

}
