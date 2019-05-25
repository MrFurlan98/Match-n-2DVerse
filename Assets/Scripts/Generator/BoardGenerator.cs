using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator  {

    public static string PATH_MODEL = "/Data/Models/model.json";
    public static string PATH_SCENARIO = "/Data/Models/scenario.json";
    public static string PATH_Type = "/Data/Models/type.json";
    [SerializeField]
    private Board m_pBoard;
    [SerializeField]
    private static int Width = 7;
    [SerializeField]
    private static int Heigth = 10;

    public static float noiseScale= 4f;

    public static int octaves=4;
    public static float persistence=1;
    public static float lacunarity=1;
    public static int seed=1;
    public static Vector2 offset = new Vector2(0,0);

    public static int[,] GenerateBoard(int seed)
    {
        float[,] noiseBoard = Noise.GenerateNoiseMap(Width,Heigth, seed,noiseScale,octaves,persistence,lacunarity,offset);
        int[,] modelBoard = new int [Width,Heigth];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Heigth; j++)
            {
                modelBoard[i, j] = noiseBoard[i, j] > 0.3 ? 1 : 0;

            }
        }
        return Mirror(modelBoard, true, true);
    }
    public static List<int[,]> Models(int qtd ,bool vertical,bool horizontal)
    {
        List<int[,]> models = new List<int[,]>();
        int i = 0;
        int j = 0;
        while(i<qtd)
        {
            int[,] validation = GenerateBoard(j);
            if (Validador(validation))
            {       
                if (models.IndexOf(validation) == -1)
                {
                    models.Add(validation);
                    i++;
                }    
            }
            j++;
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
        if(cont0 >= 0.6f * model.GetLength(0) * model.GetLength(1))
        {
            int newcont0 = 0;
            for (int i = 0; i < model.GetLength(0); i++)
            {
                for (int j = 0; j < model.GetLength(1); j++)
                {
                    if (model[i, j] == 1)
                    {
                        model[i, j] = 0;
                        newcont0++;
                    }
                    else
                    {
                        model[i, j] = 1;
                    }
                }
            }
            if (newcont0 <= 0.3f * model.GetLength(0) * model.GetLength(1))
            {
                return false;
            }
            return true;
        }
        
        if (cont0 <= 0.4f * model.GetLength(0) * model.GetLength(1) && cont0 >= 0.3f * model.GetLength(0) * model.GetLength(1))
        {
            return true;
        }
        return false;
    }
}
