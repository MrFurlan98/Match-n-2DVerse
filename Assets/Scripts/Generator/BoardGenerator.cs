using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator  {

    public static string PATH_MODEL = "/Data/Models/model.json";
    [SerializeField]
    private Board m_pBoard;
    [SerializeField]
    private static int Width = 7;
    [SerializeField]
    private static int Heigth = 10;

    public static float noiseScale= 4f;

    public static int[,] GenerateBoard()
    {
        float[,] noiseBoard = Noise.GenerateNoiseMap(Width,Heigth, noiseScale);
        int[,] modelBoard = new int [Width,Heigth];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Heigth; j++)
            {
                modelBoard[i, j] = noiseBoard[i, j] > 0.3 ? 1 : 0;

            }
        }
        return modelBoard;
    }
    public static List<int[,]> Models(int qtd)
    {
        List<int[,]> models = new List<int[,]>();
        int i = 0;
        while(i<qtd)
        {
            int[,] validation = GenerateBoard();
            if(Validador(validation))
            {
                if(models.IndexOf(validation) == -1)
                {
                    models.Add(validation);
                    i++;
                }    
            }       
        }
        return models;
    }

    public static int[,] Mirror(int[,] pModel,bool vertical,bool horizontal)
    {
        if(horizontal)
        {
            for (int i = 0; i < pModel.GetLength(0); i++)
            {
                for (int j = 0; j < pModel.GetLength(1); j++)
                {
                    if(pModel[i,j] == 0)
                    {
                        int aux = pModel.GetLength(0) - i - 1;
                        pModel[aux, j] = 0;
                    }
                }
            }
        }
        if (vertical)
        {
            for (int i = 0; i < pModel.GetLength(0); i++)
            {
                for (int j = 0; j < pModel.GetLength(1); j++)
                {
                    if (pModel[i, j] == 0)
                    {
                        int aux = pModel.GetLength(1) - j - 1;
                        pModel[i, aux] = 0;
                    }
                }
            }
        }
        return pModel;
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

    public static bool Validador(int[,] model)
    {
        int cont0 = 0;
        for (int i = 0; i < model.GetLength(0); i++)
        {
            for (int j = 0; j < model.GetLength(1); j++)
            {
                if(model[i,j]==0)
                {
                    cont0++;
                }
            }
        }
        if(cont0 > 0.3f * model.GetLength(0) * model.GetLength(1))
        {
            return false;
        }
        return true;
    }
}
