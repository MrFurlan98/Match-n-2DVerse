using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ModelViewer : MonoBehaviour {

    public List<Level> m_levels;
    [SerializeField]
    public int index=0;

    private void OnDrawGizmosSelected()
    {
        if (BoardManager.Instance.Levels[index] == null)
            return;
        int[,] currentModel = BoardManager.Instance.Levels[index].Model;
        for (int i = 0; i < currentModel.GetLength(0); i++)
        {
            for (int j = 0; j < currentModel.GetLength(1); j++)
            {
                Vector3 position = new Vector3(i, j, 0);
                if(currentModel[i,j]==0)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.blue;
                }
                Gizmos.DrawCube(position, Vector3.one * 0.5f);
            }
        }
        
    }
}
