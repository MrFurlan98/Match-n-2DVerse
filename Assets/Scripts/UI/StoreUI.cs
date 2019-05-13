using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{

    [SerializeField]
    private Button m_ExitButton;

    [SerializeField]
    private Button m_RButton;

    [SerializeField]
    private Button m_LButton;

    private void Start()
    {
        m_ExitButton.onClick = new Button.ButtonClickedEvent();
        m_ExitButton.onClick.AddListener(
            delegate
            {
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.STORE);
            });


        m_RButton.onClick = new Button.ButtonClickedEvent();
        //m_RButton.onClick.AddListener();



        m_LButton.onClick = new Button.ButtonClickedEvent();
        //m_LButton.onClick.AddListener();


    }
}