using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour {

    [SerializeField]
    private Board m_pBoard;

    public float noiseScale;

    public void GenerateBoard()
    {
        float[,] noiseBoard = Noise.GenerateNoiseMap(m_pBoard.Width, m_pBoard.Heigth, noiseScale);


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
