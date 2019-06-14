using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoad : MonoBehaviour {

    [SerializeField]
    private GameObject m_PrefabBaseRoadSettings;

    void Start () {
        int cont = 0;
        for (int i = 0; i < 60; i++)
        {
            GameObject newRoad = Instantiate(m_PrefabBaseRoadSettings,this.transform);
            newRoad.transform.SetAsFirstSibling();
            Road troad = newRoad.GetComponent<Road>();
            for (int j = 0; j < 5; j++)
            {
                troad.Levels[j].IndexButton = cont;
                cont++;
            }
        }
	}

}
