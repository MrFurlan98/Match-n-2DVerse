using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator  {

    [SerializeField]
    private Board m_pBoard;
    [SerializeField]
    private static int Width = 7;
    [SerializeField]
    private static int Heigth = 14;

    public static float noiseScale=0.5f;

    public static int[,] GenerateBoard()
    {
        float[,] noiseBoard = Noise.GenerateNoiseMap(Width,Heigth, noiseScale);
        int[,] modelBoard = new int [Width,Heigth];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Heigth; j++)
            {

                modelBoard[i, j] = noiseBoard[i, j] > 0.5 ? 1 : 0;
                
            }
        }
        return modelBoard;
    }
    public static List<int[,]> Models(int qtd)
    {
        List<int[,]> models = new List<int[,]>();
        for (int i = 0; i < qtd; i++)
        {
            models[i] = GenerateBoard();
        }
        return models;
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
}
