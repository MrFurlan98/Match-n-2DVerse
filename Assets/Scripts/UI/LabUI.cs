using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabUI : MonoBehaviour {

    [SerializeField]
    private Button m_ExitButton;

    [SerializeField]
    private Button m_HeadButton;

    [SerializeField]
    private Button m_BodyButton;

    [SerializeField]
    private Button m_ArmButton;

    [SerializeField]
    private Button m_LegsButton;

    [SerializeField]
    private Button m_RButton;

    [SerializeField]
    private Button m_LButton;

    [SerializeField]
    private Button m_ConfirmPartButton;

    private int atualChild=0;

    public Sprite[] headsParts;
    public Sprite[] legsParts;
    public Sprite[] armsParts;
    public Sprite[] bodyParts;
    public Sprite[] partUsing;

    public Image bodyPart;

    private int atualPart = 0;
    private int numberPart = 1;

    private void Start()
    {

        partUsing = CreateArray(headsParts);

        m_ExitButton.onClick = new Button.ButtonClickedEvent();
        m_ExitButton.onClick.AddListener(
            delegate
            {
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.BUTTONS.LAB);
            });

        m_HeadButton.onClick = new Button.ButtonClickedEvent();
        m_HeadButton.onClick.AddListener(
            delegate
            {
                bodyPart.sprite = m_HeadButton.GetComponent<Image>().sprite;
                atualPart = 0;
                partUsing=CreateArray(headsParts);
                numberPart = 1;
            });

        m_BodyButton.onClick = new Button.ButtonClickedEvent();
        m_BodyButton.onClick.AddListener(
            delegate
            {
                bodyPart.sprite = m_BodyButton.GetComponent<Image>().sprite;
                atualPart = 0;
                partUsing=CreateArray(bodyParts);
                numberPart = 2;
            });

        m_ArmButton.onClick = new Button.ButtonClickedEvent();
        m_ArmButton.onClick.AddListener(
            delegate
            {
                bodyPart.sprite = m_ArmButton.GetComponent<Image>().sprite;
                atualPart = 0;
                partUsing=CreateArray(armsParts);
                numberPart = 3;
            });

        m_LegsButton.onClick = new Button.ButtonClickedEvent();
        m_LegsButton.onClick.AddListener(
            delegate
            {
                bodyPart.sprite = m_LegsButton.GetComponent<Image>().sprite;
                atualPart = 0;
                partUsing=CreateArray(legsParts);
                numberPart = 4;
            });

        m_ConfirmPartButton.onClick = new Button.ButtonClickedEvent();
        m_ConfirmPartButton.onClick.AddListener(
            delegate
            {
                if (numberPart == 1) m_HeadButton.GetComponent<Image>().sprite = partUsing[atualPart];
                if (numberPart == 2) m_BodyButton.GetComponent<Image>().sprite = partUsing[atualPart];
                if (numberPart == 3) m_ArmButton.GetComponent<Image>().sprite = partUsing[atualPart];
                if (numberPart == 4) m_LegsButton.GetComponent<Image>().sprite = partUsing[atualPart];
            });

        m_RButton.onClick = new Button.ButtonClickedEvent();
        m_RButton.onClick.AddListener(
            delegate
            {
                atualPart++;
                if (atualPart == partUsing.Length) atualPart -= partUsing.Length;
                bodyPart.sprite = partUsing[atualPart];

            });


        m_LButton.onClick = new Button.ButtonClickedEvent();
        m_LButton.onClick.AddListener(
            delegate
            {
                atualPart--;
                if (atualPart == 0) atualPart += partUsing.Length;
                bodyPart.sprite = partUsing[atualPart];
            });
    }

    private Sprite[] CreateArray(Sprite[] ch)
    {
        Sprite[] instPartUsing = new Sprite[ch.Length];
        for(int i=0;  i < (ch.Length); i++)
        {
            instPartUsing[i] = ch[i];
        }

        return instPartUsing;
    }

}
