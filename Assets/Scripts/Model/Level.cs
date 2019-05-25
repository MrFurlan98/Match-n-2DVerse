using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Level{ 

    [SerializeField]
    private List<int[,]> model;
    [SerializeField]
    private string[] nivel;

    private string[] m_Type;

    public List<int[,]> Model
    {
        get
        {
            return model;
        }

        set
        {
            model = value;
        }
    }

    public string[] Nivel
    {
        get
        {
            return nivel;
        }

        set
        {
            nivel = value;
        }
    }

    public string[] Type
    {
        get
        {
            return m_Type;
        }

        set
        {
            m_Type = value;
        }
    }
}
