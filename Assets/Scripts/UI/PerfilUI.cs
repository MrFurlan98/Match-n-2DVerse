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

    public void setPerfilMembers(Image m_HeadButton, Image m_LegsButton, Image m_ArmButton, Image m_BodyButton)
    {
        m_HeadPerfilAvatar.sprite = m_HeadButton.GetComponent<Image>().sprite;
        m_LegPerfilAvatar.sprite = m_LegsButton.GetComponent<Image>().sprite;
        m_ArmPerfilAvatar.sprite = m_ArmButton.GetComponent<Image>().sprite;
        m_BodyPerfilAvatar.sprite = m_BodyButton.GetComponent<Image>().sprite;
    }
}
