using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GlowingSword : MonoBehaviour {

    [SerializeField]
    private Image sprite;

    private float H,S,V;

    public Image Sprite
    {
        get
        {
            return sprite;
        }

        set
        {
            sprite = value;
        }
    }

    private void Update()
    {
        Color.RGBToHSV(Sprite.color, out H, out S, out V);

        Sprite.color = Color.HSVToRGB(H, S, (((float)ScoreManager.Instance.Points/ (float)ScoreManager.Instance.GoalPoints)));
    }
}
