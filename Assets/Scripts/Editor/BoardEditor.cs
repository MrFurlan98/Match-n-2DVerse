using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BoardEditor : EditorWindow
{
    public int qtd;
    
    List<Level> m_BoardModels;


    public List<Level> BoardModels
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
        
        string model = Newtonsoft.Json.JsonConvert.SerializeObject(BoardModels);
        
        File.WriteAllText(Application.dataPath + "/Resources/" + BoardGenerator.PATH_MODEL + ".json", model);
    }
}
