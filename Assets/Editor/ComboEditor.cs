using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ComboEditor : EditorWindow{

    bool hidden = true;
    static ComboEditor m_ComboEditor;

    GameObject m_IconObject1;

    GameObject m_IconObject2;
    
    [MenuItem("Window/ Match N/ Combo Editor")]
    static void INIT()
    {
        m_ComboEditor = GetWindow<ComboEditor>();
        m_ComboEditor.Show();
    }

    private void OnGUI()
    {

        m_IconObject1 = (GameObject)EditorGUILayout.ObjectField(m_IconObject1, typeof(GameObject));

        m_IconObject2 = (GameObject)EditorGUILayout.ObjectField(m_IconObject2, typeof(GameObject));
        // Rect tRect = new Rect(100, 100, 50, 50);

        // if(Event.current.type == EventType.MouseDown && tRect.Contains(Event.current.mousePosition))
        // {
        //     m_Icon1 = EditorGUILayout.ObjectField()
        // }

        if (m_IconObject1 == null || m_IconObject2 == null)
            return;

        Icon m_Icon1 = m_IconObject1.GetComponent<Icon>();


        Icon m_Icon2 = m_IconObject2.GetComponent<Icon>();


        // GUILayout.BeginArea(tRect, EditorStyles.helpBox);
        if (m_Icon1 != null)
        {
            Sprite tSprite = m_Icon1.GetComponent<SpriteRenderer>().sprite;

            Texture2D tTexture = tSprite.texture;

            GUILayout.Label(tTexture, GUILayout.Width(50), GUILayout.Height(50));
        }
        if (m_Icon2 != null)
        {
            Sprite tSprite = m_Icon2.GetComponent<SpriteRenderer>().sprite;

            Texture2D tTexture = tSprite.texture;

            GUILayout.Label(tTexture, GUILayout.Width(50), GUILayout.Height(50));
        }

        //GUILayout.EndArea();
    }
}
