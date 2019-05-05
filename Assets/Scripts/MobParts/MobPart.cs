using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MobPart", menuName ="Create Part")]
public class MobPart : ScriptableObject {

    public Sprite imagePart;
    public bool isDefaultItem = false;
    public int numberPart;
}
