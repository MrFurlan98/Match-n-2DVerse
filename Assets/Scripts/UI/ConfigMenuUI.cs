using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigMenuUI : MonoBehaviour {

    [SerializeField]
    private Button m_ExitButton;

    private void Start()
    {
        m_ExitButton.onClick = new Button.ButtonClickedEvent();
        m_ExitButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.BUTTONS.CONFIGURATIONS);
            });

    }

}
