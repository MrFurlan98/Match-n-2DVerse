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
        Color.RGBToHSV(new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, Sprite.color.a), out H, out S, out V);

        Sprite.color = Color.HSVToRGB(H, S, (V + (ScoreManager.Instance.Points * (70 / ScoreManager.Instance.GoalPoints))));
    }
}
