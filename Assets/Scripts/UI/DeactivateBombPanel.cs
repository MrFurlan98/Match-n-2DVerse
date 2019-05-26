using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DeactivateBombPanel : MonoBehaviour {

    [SerializeField]
    private Text m_tScore;

    [SerializeField]
    private Text m_tTimer;

    private bool setSprites = true;
    [SerializeField]
    private List<Image> m_TargetSprites = new List<Image>();

    private void Update()
    {
        if(ScoreManager.Instance.StartGame)
        {
            if (setSprites)
            {
                SetSprites();
                setSprites = false;
            }
        }
        
        if(ScoreManager.Instance !=null)
        {

            m_tScore.text = ScoreManager.Instance.Points.ToString();

            Timer.text = ScoreManager.Instance.Timer.ToString("0");

            if (ScoreManager.Instance.StartGame)
            {
                int cont = 0;
                for (int i = 0; i < ScoreManager.Instance.NumberToDestroy.Count; i++)
                {
                    if (ScoreManager.Instance.NumberToDestroy[i] <= 0)
                    {
                        TargetSprites[i].enabled = false;
                        cont++;
                    }
                }
                if (cont == ScoreManager.Instance.NumberToDestroy.Count)
                {
                    
                    ScoreManager.Instance.StopTime(float.Parse(Timer.text));
                    UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.POPUP);
                    PopUpUI.Instance.OpenPopUp("Fim de jogo", true, GamePlayUI.BackMenu);
                    ScoreManager.Instance.StartGame = false;
                    setSprites = true;
                }
                if (ScoreManager.Instance.Timer <= 0)
                {
                    UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.POPUP);
                    PopUpUI.Instance.OpenPopUp("Fim de jogo", false, GamePlayUI.BackMenu);
                    ScoreManager.Instance.StartGame = false;
                    setSprites = true;
                }
                cont = 0;
            }
        }
        
    }
    private void SetSprites()
    {
        List<Sprite> tSprite = IconManager.Instance.ShambleIconSprite();
        GameManager.Instance.m_IconToDestroy = IconManager.Instance.GetModulos(tSprite);
        int qtd = ScoreManager.Instance.NumberToDestroy.Count;
        for (int i = 0; i < qtd; i++)
        {
            TargetSprites[i].sprite = tSprite[i];

        }
        if (qtd != IconManager.Instance.Modulos.Count)
        {
            for (int i = qtd; i < IconManager.Instance.Modulos.Count; i++)
            {
                TargetSprites[i].enabled = false;
            }
        }
    }
    public Text TScore
    {
        get
        {
            return m_tScore;
        }

        set
        {
            m_tScore = value;
        }
    }

    public Text Timer
    {
        get
        {
            return m_tTimer;
        }

        set
        {
            m_tTimer = value;
        }
    }


    public List<Image> TargetSprites
    {
        get
        {
            return m_TargetSprites;
        }

        set
        {
            m_TargetSprites = value;
        }
    }
}
