using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerfilUI : MonoBehaviour {

    public static PerfilUI instance;
    [SerializeField]
    private Button m_ExitButton;

    #region Avatar
    [SerializeField]
    private Image m_HeadPerfilAvatar;
    [SerializeField]
    private Image m_ArmPerfilAvatar;
    [SerializeField]
    private Image m_LegPerfilAvatar;
    [SerializeField]
    private Image m_BodyPerfilAvatar; 
    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        m_ExitButton.onClick = new Button.ButtonClickedEvent();
        m_ExitButton.onClick.AddListener(
            delegate
            {
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.PERFIL);
            });

    }

    public void setPerfilMembers(Sprite m_HeadButton, Sprite m_LegsButton, Sprite m_ArmButton, Sprite m_BodyButton)
    {
        m_HeadPerfilAvatar.sprite = m_HeadButton;
        m_LegPerfilAvatar.sprite = m_LegsButton;
        m_ArmPerfilAvatar.sprite = m_ArmButton;
        m_BodyPerfilAvatar.sprite = m_BodyButton;
    }
}
