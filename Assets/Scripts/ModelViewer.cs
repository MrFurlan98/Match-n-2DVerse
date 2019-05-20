using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ModelViewer : MonoBehaviour {

    List<int[,]> m_levels;
    [SerializeField]
    public int index=0;

    private void Start()
    {
        string model = File.ReadAllText(Application.dataPath + BoardGenerator.PATH_MODEL);
        
        m_levels = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int[,]>>(model);
    }
    private void OnDrawGizmosSelected()
    {
        if (m_levels == null)
            return;
        int[,] currentModel = m_levels[index];
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
