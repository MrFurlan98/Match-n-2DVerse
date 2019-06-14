using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator  {

    public static string PATH_MODEL = "Data/Models/model";
    public static string PATH_SCENARIO = "Data/Models/scenario.json";
    public static string PATH_Type = "Data/Models/type.json";
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
    public static List<Level> Models(int qtd ,bool vertical,bool horizontal)
    {
        List<Level> models = new List<Level>();
        System.Random prng = new System.Random(seed);
        int a = 0;
        int[] array = { 1, 2, 3, 4 };
        shuffleArray(array);
        for (int w = 0; w < qtd; w++)
        {
            Level level = new Level();
            models.Add(level);
            if (a==4)
            {
                shuffleArray(array);
                a = 0;
            }
            Debug.Log(array[a]);
            if (array[a] == 1)
            {
                models[w].Scenario = "APOCALIPTICO";
                models[w].Type = "Resgate";
            }
            if (array[a] == 2)
            {
                models[w].Scenario = "APOCALIPTICO";
                models[w].Type = "Desativar_Bomba";
            }
            if (array[a] == 3)
            {
                models[w].Scenario = "GREGO";
                models[w].Type = "Sobre_O_Olhar_Da_Gorgona";
            }
            if (array[a] == 4)
            {
                models[w].Scenario = "GREGO";
                models[w].Type = "Um_Dos_Doze_Trabalhos";
            }
            a++;
        }
        for (int k = 0; k < qtd; k++)
        {
            models[k].GoalPoints = 10000;
            models[k].MovesLeft = 12;
            if (k <= qtd / 5)
            {
                models[k].TargetLeft = 1;
            }
            if (k <= qtd * 2 / 5 && k > qtd / 5)
            {
                models[k].TargetLeft= 2;
            }
            if (k <= qtd * 3 / 5 && k > qtd * 2 / 5)
            {
                models[k].TargetLeft = 3;
            }
            if (k <= qtd * 4 / 5 && k > qtd * 3 / 5)
            {
                models[k].TargetLeft = 4;
            }
            if (k <= qtd && k > qtd * 4 / 5)
            {
                models[k].TargetLeft = 5;
            }
        }
        int i = 0;
        int j = 0;
        while (i < qtd)
        {
            int[,] validation = GenerateBoard(j);
            if (Validador(validation,models[i]))
            {

                models[i].Model = validation;
                i++;
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
    public static void shuffleArray(int[] array)
    {
        System.Random prng = new System.Random(100);
        for (int i = 0; i < array.Length; i++)
        {
            swap(array, i, i + prng.Next(array.Length - i));
        }
    }
    public static void swap(int[]array,int a,int b)
    {
        int temp = array[a];
        array[a] = array[b];
        array[b] = temp;
    }
    public static bool Validador(int[,] model,Level level)
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
        if(level.Type == "Um_Dos_Doze_Trabalhos")
        {
            
            for (int i = 0; i < model.GetLength(0); i++)
            {
                if(model[i,0]==0)
                {
                    return false;
                }
            }
            int cont = 0;
            for (int i = 0; i < model.GetLength(0); i++)
            {
                if(model[i,model.GetLength(1)-1]==0)
                {
                    cont++;
                }
            }
            if(model.GetLength(1)-cont < 5)
            {
                return false;
            }
        }
        if (cont0 >= 0.6f * model.GetLength(0) * model.GetLength(1))
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
