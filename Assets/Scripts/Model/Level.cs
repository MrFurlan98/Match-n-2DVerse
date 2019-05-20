using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Level", menuName = "New Level")]
public class Level : ScriptableObject {

    public int height;
    public int width;

    public BoardIcon[,] m_Icons;
}
