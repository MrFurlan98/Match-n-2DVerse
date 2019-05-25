using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BoardEditor : EditorWindow
{
    public int qtd;
    
    List<int[,]> m_BoardModels;

    string[] m_BoardTypes;

    string[] m_BoardScenario;

    public List<int[,]> BoardModels
    {
        get
        {
            return m_BoardModels;
        }

        set
        {
            m_BoardModels = value;
        }
    }


    public string[] BoardTypes
    {
        get
        {
            return m_BoardTypes;
        }

        set
        {
            m_BoardTypes = value;
        }
    }

    public string[] BoardScenario
    {
        get
        {
            return m_BoardScenario;
        }

        set
        {
            m_BoardScenario = value;
        }
    }

    [MenuItem("Window/ Match N/ Board Editor")]

    public static void Init()
    {
        BoardEditor tBoardEditor = GetWindow<BoardEditor>();

        tBoardEditor.minSize = new Vector2(450, 600);
        tBoardEditor.maxSize = new Vector2(450, 600);

        tBoardEditor.Show();
    }


    private void OnGUI()
    {

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        EditorGUILayout.BeginVertical();
        qtd = EditorGUILayout.IntField(qtd);
        
        if(GUILayout.Button("EXPORT"))
        {
            Export();
        }

        EditorGUILayout.EndVertical();

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    private void Export()
    {
        BoardModels = BoardGenerator.Models(qtd,true,true);
        BoardScenario = new string[qtd];
        BoardTypes = new string[qtd];
        int a = 0;
        int b = 0;
        int seed =1;
        for (int i = 0; i < qtd/2; i++)
        {
            if(b%2==0)
            {
                if (a < 2)
                {
                    BoardScenario[i] = "APOCALIPTICO";
                }
                if (a == 2)
                {
                    BoardScenario[i] = "GREGO";
                }
                if (a > 2)
                {
                    BoardScenario[i] = "APOCALIPTICO";
                    b++;
                }
                a++;
                if (a == 4)
                {
                    a = 0;
                }
            }
            else
            {
                if (a < 2)
                {
                    BoardScenario[i] = "GREGO";
                }
                if (a == 2)
                {
                    BoardScenario[i] = "APOCALIPTICO";
                }
                if (a > 2)
                {
                    BoardScenario[i] = "GREGO";
                    b++;
                }
                a++;
                if (a == 4)
                {
                    a = 0;
                }
            }
        }
        a = 0;
        for (int i = qtd/2; i < qtd; i++)
        {
            if(a<2)
            {
                BoardScenario[i] = "APOCALIPTICO";
                a++;
            }
            else
            {
                a++;
                BoardScenario[i] = "GREGO";
                if(a==4)
                {
                    a = 0;
                }
            }
        }
        System.Random prng = new System.Random(seed);
        for (int i = 0; i < qtd; i++)
        {
            if(BoardScenario[i]== "APOCALIPTICO")
            {
                if(prng.Next(0,2)==0)
                {
                    BoardTypes[i] = "Resgate";
                }
                else
                {
                    BoardTypes[i] = "Desativar_Bomba";
                }
            }
            else
            {
                if(prng.Next(0,2)==0)
                {
                    BoardTypes[i] = "Um_Dos_Doze_Trabalhos";
                }
                else
                {
                    BoardTypes[i] = "Sobre_O_Olhar_Da_Gorgona";
                }
            }
        }
        string boardTypes = Newtonsoft.Json.JsonConvert.SerializeObject(BoardTypes);
        string scenario = Newtonsoft.Json.JsonConvert.SerializeObject(BoardScenario);
        string model = Newtonsoft.Json.JsonConvert.SerializeObject(BoardModels);
        File.WriteAllText(Application.dataPath + BoardGenerator.PATH_Type, boardTypes);
        File.WriteAllText(Application.dataPath + BoardGenerator.PATH_MODEL, model);
        File.WriteAllText(Application.dataPath + BoardGenerator.PATH_SCENARIO, scenario);
    }
}
